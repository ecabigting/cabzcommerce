using cabzcommerce.api.Helpers;
using BCryptNet = BCrypt.Net.BCrypt;
using cabzcommerce.cshared.DTOs.User;
using cabzcommerce.cshared.Models;
using MongoDB.Driver;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using cabzcommerce.cshared.DTOs.Product;

namespace cabzcommerce.api.Repositories
{
    public class ProductRepo : IProductRepo
    {
        private ApiSettings aSettings;
        private readonly IMongoCollection<Product> productsCollection;
        private readonly FilterDefinitionBuilder<Product> filterBuilder = Builders<Product>.Filter;
        public ProductRepo(IMongoClient _mongoClient,DBSettings _dbSettings,ApiSettings _apiSettings)
        {
            IMongoDatabase db = _mongoClient.GetDatabase(_dbSettings.DbName);
            productsCollection = db.GetCollection<Product>("Products");
            aSettings = _apiSettings;
        }

        public Task<Product> Add(ProductDto Product, Guid OwnerId)
        {
            throw new NotImplementedException();
        }

        public Task<Product> Update(Product Product)
        {
            throw new NotImplementedException();
        }

        public Task<ProductCustomDescription> AddProductCustomDescription(ProductCustomDescription ProductDescription)
        {
            throw new NotImplementedException();
        }
    }
}