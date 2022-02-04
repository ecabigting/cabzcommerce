using cabzcommerce.cshared.DTOs;
using cabzcommerce.cshared.DTOs.User;
using cabzcommerce.cshared.Models;

namespace cabzcommerce.api.Repositories
{
    public interface IUserRepo
    {
        Task<User> Register(Registration User);
        Task<User> Login(Login User);
    }

}