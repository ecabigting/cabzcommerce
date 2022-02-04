
namespace cabzcommerce.api.Helpers
{
    public class DBSettings 
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string DbName { get; set; }

        public string ConnString { 
            get
            {
                return $"mongodb+srv://{User}:{Password}@{Host}/{DbName}?retryWrites=true&w=majority";
            } 
        }
    }    
}