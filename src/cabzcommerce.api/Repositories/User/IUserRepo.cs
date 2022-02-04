using cabzcommerce.cshared.DTOs;
using cabzcommerce.cshared.DTOs.User;

namespace cabzcommerce.api.Repositories.User
{
    public interface IUserRepo
    {
        Task<ApiResponse> Register(Registration User);
        Task<ApiResponse> Login(Login User);
    }

}