using cabzcommerce.cshared.DTOs;
using cabzcommerce.cshared.DTOs.User;
using cabzcommerce.cshared.Models;

namespace cabzcommerce.api.Repositories
{
    public interface IUserRepo
    {
        Task<Profile> Register(Registration User);
        Task<UserAccessToken> GrantUserAccess(User User);
        Task<User> GetUser(Guid Id);
        Task<User> GetUserByEmail(string Email);
    }

}