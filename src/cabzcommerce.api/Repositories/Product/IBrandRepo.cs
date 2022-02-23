using cabzcommerce.cshared.Models;

namespace cabzcommerce.api.Repositories
{
    public interface IBrandRepo
    {
        Task<Brand> Add(Brand brand);
        Task<Brand> Update(Brand brand);
        Task<bool> BrandNameExist(string name);
        Task<Brand> GetBrandByID(Guid BrandId);
        Task<bool> CheckBrandNameWithId(string name,Guid id);
        Task<List<Brand>> GetAllBrands();
    }
}