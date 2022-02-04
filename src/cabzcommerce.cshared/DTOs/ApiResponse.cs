namespace cabzcommerce.cshared.DTOs
{
    public class ApiResponse 
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public Object Data {get;set;}
        public string ErrorMessage { get; set; }
    }
}