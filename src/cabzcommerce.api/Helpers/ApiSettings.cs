namespace cabzcommerce.api.Helpers
{

    public class ApiSettings 
    {
        public string HashKey { get; set; }
        public string Issuer { get; set; }
        public string Audience {get;set;}
        public string JWTKey { get; set; }
        public int TokenExp { get; set; }
        public int RefreshTokenExp { get; set; }
    }    
}