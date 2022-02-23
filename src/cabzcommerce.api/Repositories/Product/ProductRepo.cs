using cabzcommerce.api.Helpers;
using cabzcommerce.cshared.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace cabzcommerce.api.Repositories
{
    public class ProductRepo : IProductRepo
    {
        private readonly IMongoCollection<Product> productsCollection;
        private readonly FilterDefinitionBuilder<Product> filterBuilder = Builders<Product>.Filter;
        public ProductRepo(IMongoClient _mongoClient,DBSettings _dbSettings)
        {
            IMongoDatabase db = _mongoClient.GetDatabase(_dbSettings.DbName);
            productsCollection = db.GetCollection<Product>("Products");
        }

        public async Task<bool> ProductNameExist(string Name)
        {
            var filter = filterBuilder.Eq(i => i.Name, Name);
            return await productsCollection.Find(filter).CountDocumentsAsync() > 0 ? true : false;
        }

        public async Task<bool> CheckProductNameWithId(string name,Guid id)
        {
            var filter = filterBuilder.Where(b => b.Name == name && b.Id != id);
            return await productsCollection.Find(filter).CountDocumentsAsync() > 0 ? true : false;
        }
        public async Task<List<Product>> GetAllProducts()
        {
            return await productsCollection.Find(new BsonDocument()).ToListAsync();
        }
        public async Task<Product> GetProductByID(Guid Id)
        {
            var filter = filterBuilder.Eq(i => i.Id, Id);
            return await productsCollection.Find(filter).SingleOrDefaultAsync();
        }
        // public async Task<Product> Add(ProductDto Product,Guid OwnerId)
        // {

        // }
        // public async Task<Product> Update(Product Product)
        // {

        // }
        // public async Task<ProductCustomDescription> AddProductCustomDescription(ProductCustomDescription ProductDescription)
        // {

        // }
    }
}