using cabzcommerce.api.Helpers;
using BCryptNet = BCrypt.Net.BCrypt;
using cabzcommerce.cshared.DTOs.User;
using cabzcommerce.cshared.Models;
using MongoDB.Driver;

namespace cabzcommerce.api.Repositories
{
    public class UserRepo : IUserRepo
    {
        private const string collectionName = "Users";
        private ApiSettings aSettings;
        private readonly IMongoCollection<User> usersCollection;
        private readonly FilterDefinitionBuilder<User> filterBuilder = Builders<User>.Filter;
        public UserRepo(IMongoClient _mongoClient,DBSettings _dbSettings,ApiSettings _apiSettings)
        {
            IMongoDatabase db = _mongoClient.GetDatabase(_dbSettings.DbName);
            usersCollection = db.GetCollection<User>(collectionName); 
            aSettings = _apiSettings;

        }
        public async Task<User> Login(Login User)
        {
            throw new NotImplementedException();
        }

        public async Task<Profile> Register(Registration _user)
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
                UpdatedDateTime = DateTimeOffset.UtcNow,
                Password = BCryptNet.HashPassword(_user.Password),
            };
            await usersCollection.InsertOneAsync(NewUser);
            return new Profile {
                DateOfBirth = NewUser.DateOfBirth,
                Email = NewUser.Email,
                FirstName =NewUser.Email,
                LastName = NewUser.LastName,
                PhoneNumber = NewUser.PhoneNumber,
                UserType =NewUser.UserType,
                UserAccess = null
            };
        }
    
        public async Task<User> GetUser(Guid Id)
        {
            var filter = filterBuilder.Eq(i => i.Id, Id);
            return await usersCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<User> GetUserByEmail(string Email)
        {
            var filter = filterBuilder.Eq(i => i.Email, Email);
            return await usersCollection.Find(filter).SingleOrDefaultAsync();
        }
    }
}