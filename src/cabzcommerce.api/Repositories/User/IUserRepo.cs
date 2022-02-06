using cabzcommerce.cshared.DTOs;
using cabzcommerce.cshared.DTOs.User;
using cabzcommerce.cshared.Models;

namespace cabzcommerce.api.Repositories
{
    public interface IUserRepo
    {
        Task<Profile> Register(Registration User);
        Task<User> Login(Login User);
        Task<User> GetUser(Guid Id);
        Task<User> GetUserByEmail(string Email);
    }

}