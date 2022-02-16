using cabzcommerce.api.Helpers;
using cabzcommerce.cshared.Models;
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

        public Task<Brand> Add(Brand brand)
        {
            throw new NotImplementedException();
        }

        public Task<Brand> Update(Brand brand)
        {
            throw new NotImplementedException();
        }

        public Task<Brand> Delete(Brand brand)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> BrandNameExist(string name)
        {
            var filter = filterBuilder.Eq(i => i.Name, name);
            return await brandsCollection.Find(filter).CountDocumentsAsync() > 0 ? true : false;
        }
    }
}