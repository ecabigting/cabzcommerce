using System.ComponentModel.DataAnnotations;

namespace cabzcommerce.cshared.DTOs.User
{
    public class Registration 
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
        public string[] UserType { get; set; }   
    }
}

