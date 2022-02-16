using cabzcommerce.cshared.DTOs.Product;
using cabzcommerce.cshared.Models;

namespace cabzcommerce.api.Repositories
{
    public interface IBrandRepo
    {
        Task<Brand> Add(Brand brand);
        Task<Brand> Update(Brand brand);
        Task<Brand> Delete(Brand brand);
        Task<bool> BrandNameExist(string name);
    }

}