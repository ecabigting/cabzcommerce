using System.ComponentModel.DataAnnotations;
using cabzcommerce.cshared.Models;

namespace cabzcommerce.cshared.DTOs.Product
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int MyProperty { get; set; }
        public string ProductImgeDesktopUrl { get; set; }
        public string ProductImgMobileUrl { get; set; }
        public List<ProductType> Type { get; set; }    
    
    
    }
}