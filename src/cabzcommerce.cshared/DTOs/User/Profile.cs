using System.ComponentModel.DataAnnotations;
using cabzcommerce.cshared.Models;

namespace cabzcommerce.cshared.DTOs.User
{
    public class Profile 
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTimeOffset DateOfBirth { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public UserType UserType { get; set; }   
        public Access UserAccess{get;set;}
    }
}
