using CSHP330RestAPIProject.Models;
using System.Collections.Generic;

namespace CSHP330RestAPIProject.Repository
{
    public interface IUserRepository
    {
        List<User> UserList { get; set; }
    }
}