using CSHP330RestAPIProject.Models;
using System.Collections.Generic;

namespace CSHP330RestAPIProject.Repository
{
    public class UserRepository : IUserRepository
    {
        public List<User> UserList { get; set; }
    }
}
