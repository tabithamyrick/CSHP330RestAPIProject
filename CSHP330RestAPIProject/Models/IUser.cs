using System;

namespace CSHP330RestAPIProject.Models
{
    public interface IUser
    {
        DateTime CreatedDate { get; set; }
        int Id { get; set; }
        string UserEmail { get; set; }
        string UserPassword { get; set; }
    }
}