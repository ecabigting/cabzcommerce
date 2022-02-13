using cabzcommerce.cshared.DTOs;
using cabzcommerce.cshared.DTOs.Product;
using cabzcommerce.cshared.DTOs.User;
using cabzcommerce.cshared.Models;

namespace cabzcommerce.api.Repositories
{
    public interface IProductRepo
    {
        Task<Product> Add(ProductDto Product,Guid OwnerId);
        Task<Product> Update(Product Product);
        Task<ProductCustomDescription> AddProductCustomDescription(ProductCustomDescription ProductDescription);
    }

}