using cabzcommerce.api.Helpers;
using cabzcommerce.cshared.DTOs;
using cabzcommerce.cshared.DTOs.User;
using cabzcommerce.cshared.Models;
using MongoDB.Driver;

namespace cabzcommerce.api.Repositories
{
    public class UserRepo : IUserRepo
    {
        private const string collectionName = "Users";
        private readonly IMongoCollection<User> usersCollection;
        public UserRepo(IMongoClient _mongoClient,DBSettings _settings)
        {
            IMongoDatabase db = _mongoClient.GetDatabase(_settings.DbName);
            usersCollection = db.GetCollection<User>(collectionName); 
        }
        public async Task<User> Login(Login User)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Register(Registration _user)
        {
            User NewUser = new User {
                DateOfBirth = _user.DateOfBirth,
                Email = _user.Email,
                FirstName = _user.Email,
                ImgUrl = "",
                LastName = _user.LastName,
                PhoneNumber = _user.PhoneNumber,
                UserType = new List<string>{"User"},
                CreatedDateTime = DateTimeOffset.UtcNow,
                UpdatedDateTime = DateTimeOffset.UtcNow
            };
            await usersCollection.InsertOneAsync(NewUser);
            return NewUser;
        }
    }
}