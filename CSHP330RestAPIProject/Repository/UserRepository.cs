using CSHP330RestAPIProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSHP330RestAPIProject.Repository
{
    public class UserRepository : IUserRepository
    {
        public List<User> UserList { get; set; }
    }
}
