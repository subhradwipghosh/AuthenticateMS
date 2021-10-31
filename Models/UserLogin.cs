using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticateMicroservice.Models
{
    public class UserLogin
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
        public DateTime LoginTime { get; set; }
    }
}
