using System.ComponentModel.DataAnnotations;

namespace cabzcommerce.cshared.Models 
{
    public class User : BaseClass
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
        public List<string> UserType { get; set; }   
        [Required]
        public string ImgUrl {get;set;}
        [Required]
        public string Password {get;set;}
    }

    public class UserAccessToken : BaseClass 
    {
        public string Token { get; set; }
        public Guid RefreshToken { get; set; }
        public DateTimeOffset TokenExp { get; set; }
        public DateTimeOffset RefreshTokenExp { get; set; }
        public Guid UserId {get;set;}
    }
}