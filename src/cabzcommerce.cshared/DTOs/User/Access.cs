using System.ComponentModel.DataAnnotations;

namespace cabzcommerce.cshared.DTOs.User
{
    public class Access 
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTimeOffset TokenExp { get; set; }
        public DateTimeOffset RefreshTokenExp { get; set; }
    }
}