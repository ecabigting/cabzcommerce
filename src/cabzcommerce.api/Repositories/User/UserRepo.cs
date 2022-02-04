using cabzcommerce.cshared.DTOs;
using cabzcommerce.cshared.DTOs.User;

namespace cabzcommerce.api.Repositories.User
{
    public class UserRepo : IUserRepo
    {
        public Task<ApiResponse> Login(Login User)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> Register(Registration User)
        {
            throw new NotImplementedException();
        }
    }
}