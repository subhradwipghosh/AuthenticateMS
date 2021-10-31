using AuthenticateMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AuthenticateMicroservice.Repositories
{
    public class UserListRep:IUserListRep
    {
        
        public IEnumerable<User> getUserList()
        {
            var userList = new List<User>
            {
                new User{UserId=1,Password="1234",Roles="Employee"},
                new User{UserId=2,Password="12345",Roles="Customer"},
                new User{UserId=4,Password="123456",Roles="Customer"},
                new User{UserId=6,Password="1234567",Roles="Customer"}
            };
            return userList;
        }

        public void AddToDatabase()
        {

        }
    }
}
