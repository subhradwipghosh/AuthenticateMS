using AuthenticateMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticateMicroservice.Repositories
{
    interface IUserListRep
    {
        public IEnumerable<User> getUserList();
    }
}
