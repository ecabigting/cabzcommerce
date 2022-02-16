using cabzcommerce.api.Helpers;
using BCryptNet = BCrypt.Net.BCrypt;
using cabzcommerce.cshared.DTOs.User;
using cabzcommerce.cshared.Models;
using MongoDB.Driver;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace cabzcommerce.api.Repositories
{
    public class UserRepo : IUserRepo
    {
        private ApiSettings aSettings;
        private readonly IMongoCollection<User> usersCollection;
        private readonly IMongoCollection<UserAccessToken> userAccessToken;
        private readonly FilterDefinitionBuilder<User> filterBuilder = Builders<User>.Filter;
        private readonly FilterDefinitionBuilder<UserAccessToken> filterBuilderUAT = Builders<UserAccessToken>.Filter;
        public UserRepo(IMongoClient _mongoClient,DBSettings _dbSettings,ApiSettings _apiSettings)
        {
            IMongoDatabase db = _mongoClient.GetDatabase(_dbSettings.DbName);
            usersCollection = db.GetCollection<User>("Users"); 
            userAccessToken = db.GetCollection<UserAccessToken>("UserAccessTokens");
            aSettings = _apiSettings;
        }

        public async Task<UserAccessToken> GrantUserAccess(User _user)
        {
            var filterByUserId = filterBuilderUAT.Where(uat => uat.UserId == _user.Id && uat.TokenExp > DateTimeOffset.Now);
            UserAccessToken userToken = await userAccessToken.Find(filterByUserId).SingleOrDefaultAsync();
            if(userToken == null)
            {
                var claims = new []
                {
                    new Claim(ClaimTypes.Email, _user.Email),
                    new Claim(ClaimTypes.GivenName, _user.FirstName),
                    new Claim(ClaimTypes.Surname,_user.LastName),
                    new Claim(ClaimTypes.Role,_user.ToString()),
                };
                Console.WriteLine(aSettings.HashKey);
                var token = new JwtSecurityToken
                (
                    issuer: aSettings.Issuer,
                    audience:aSettings.Audience,
                    expires: DateTime.Now.AddMinutes(aSettings.TokenExp),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(aSettings.HashKey)),
                        SecurityAlgorithms.HmacSha256
                    )
                ); 
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                userToken =  new UserAccessToken {
                    CreatedBy = _user.Id,
                    CreatedDateTime = DateTimeOffset.Now,
                    RefreshToken = Guid.NewGuid(),
                    RefreshTokenExp = DateTime.Now.AddSeconds(aSettings.RefreshTokenExp),
                    TokenExp = DateTime.Now.AddSeconds(aSettings.TokenExp),
                    UserId = _user.Id,
                    UpdatedBy = _user.Id,
                    UpdatedDateTime = DateTimeOffset.Now,
                    Token = tokenString
                };
                await userAccessToken.InsertOneAsync(userToken);
            }
            return userToken;
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
                UserType = _user.UserType,
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

        public async Task<UserAccessToken> RefreshToken(Guid RefToken, string UserToken)
        {
            var filterUAT = filterBuilderUAT.Where(uat => uat.Token == UserToken && 
            uat.RefreshToken == RefToken && 
            uat.RefreshTokenExp > DateTimeOffset.Now);
            UserAccessToken userToken = await userAccessToken.Find(filterUAT).SingleOrDefaultAsync();
            if(userToken!=null)
            {
                User GrantAccessTokenToThisUser = await GetUser(userToken.UserId);
                return await GrantUserAccess(GrantAccessTokenToThisUser);
                
            }else
            {
                return null;
            }
        }

        public async Task<UserAccessToken> GetUserAccessByCurrentToken(string UserToken)
        {
            var filterUAT = filterBuilderUAT.Eq(uat => uat.Token,UserToken);
            return await userAccessToken.Find(filterUAT).SingleOrDefaultAsync();
        }
    }
}