using System.ComponentModel.DataAnnotations;

namespace cabzcommerce.cshared.DTOs.User
{
    public class Login 
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}