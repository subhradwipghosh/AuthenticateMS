using AuthenticateMicroservice.Models;
using AuthenticateMicroservice.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthenticateMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class TokenController : ControllerBase
    {
        UserContext context;
        //readonly log4net.ILog _log4net;
        public TokenController(UserContext context)
        {
            //_log4net = log4net.LogManager.GetLogger(typeof(TokenController));
            this.context = context;
        }
        
        // POST api/<TokenController>
        [HttpPost]
        public IActionResult AuthenticateUser([FromBody] User user)
        {
            //_log4net.Info("Login method generated");
            UserListRep uL = new UserListRep();
            var userList = uL.getUserList();
            foreach (var v in userList)  //Change v to a suitable name
            {
                if (user.UserId == v.UserId && user.Password == v.Password && user.Roles == v.Roles)
                {
                    UserLogin userLogin = new UserLogin {
                        UserId = user.UserId, 
                        Password = user.Password, 
                        Roles = user.Roles, 
                        LoginTime = DateTime.Now 
                    };
                    context.UserLoginDetails.Add(userLogin);
                    context.SaveChanges();

                    string role = user.Roles;
                    /*if (user.Roles == "Employee")
                        role = "Employee";
                    else
                        role = "Customer";*/
                    var result = new
                    {
                        token = GenerateJSONWebToken(user.UserId, role)
                    };
                    //_log4net.Info("Token Generated");
                    return Ok(result);
                }
            }
            //_log4net.Info("Token Denied");
            return BadRequest();
        }

        private string GenerateJSONWebToken(int userId, string userRole)
        {
            // _log4net.Info("Token Generation Initiated");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysuperdupersecret"));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {

                new Claim(ClaimTypes.Role, userRole),

                new Claim("UserId", userId.ToString()),

                new Claim("role", userRole)

            };

            var token = new JwtSecurityToken(

            issuer: "mySystem",

            audience: "myUsers",

            claims: claims,

            expires: DateTime.Now.AddMinutes(10),

            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
