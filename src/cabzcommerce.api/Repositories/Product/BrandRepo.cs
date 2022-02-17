using cabzcommerce.api.Helpers;
using cabzcommerce.cshared.DTOs.Product;
using cabzcommerce.cshared.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace cabzcommerce.api.Repositories
{
    public class BrandRepo : IBrandRepo
    {
        private readonly IMongoCollection<Brand> brandsCollection;
        private readonly FilterDefinitionBuilder<Brand> filterBuilder = Builders<Brand>.Filter;
        public BrandRepo(IMongoClient _mongoClient,DBSettings _dbSettings)
        {
            IMongoDatabase db = _mongoClient.GetDatabase(_dbSettings.DbName);
            brandsCollection = db.GetCollection<Brand>("Brands");
        }

        public async Task<Brand> Add(Brand brand)
        {
            await brandsCollection.InsertOneAsync(brand);
            return brand;
        }

        public async Task<Brand> Update(Brand brand)
        {
            var filter = filterBuilder.Eq(i => i.Id, brand.Id);
            await brandsCollection.ReplaceOneAsync(filter,brand);
            return brand;
        }

        public async Task Delete(Guid BrandId)
        {
            var filter = filterBuilder.Eq(i => i.Id, BrandId);
            await brandsCollection.DeleteOneAsync(filter);
        }

        public async Task<Brand> GetBrandByID(Guid BrandId)
        {
            var filter = filterBuilder.Eq(i => i.Id, BrandId);
            return await brandsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<bool> BrandNameExist(string name)
        {
            var filter = filterBuilder.Eq(i => i.Name, name);
            return await brandsCollection.Find(filter).CountDocumentsAsync() > 0 ? true : false;
        }

        public async Task<bool> CheckBrandNameWithId(string name,Guid id)
        {
            var filter = filterBuilder.Where(b => b.Name == name && b.Id != id);
            return await brandsCollection.Find(filter).CountDocumentsAsync() > 0 ? true : false;
        }

        public async Task<List<Brand>> GetAllBrands()
        {
            return await brandsCollection.Find(new BsonDocument()).ToListAsync();
        }
    }
}