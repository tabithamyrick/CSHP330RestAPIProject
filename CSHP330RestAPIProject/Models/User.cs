using System;
using System.ComponentModel.DataAnnotations;

namespace CSHP330RestAPIProject.Models
{
    public class User : IUser
    {
        public int Id { get; set; }
        [Required]
        [DataType("Email")]
        public string UserEmail { get; set; }
        [Required]
        [DataType("Password")]
        public string UserPassword { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
