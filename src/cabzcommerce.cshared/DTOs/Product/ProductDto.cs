using System.ComponentModel.DataAnnotations;
using cabzcommerce.cshared.Models;

namespace cabzcommerce.cshared.DTOs.Product
{
    public class ProductDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string ProductImgeDesktopUrl { get; set; }
        [Required]
        public string ProductImgMobileUrl { get; set; }
        [Required]
        public List<ProductTypeDto> ProductType { get; set; }    
        [Required]
        public bool IsEnabled { get; set; }
    }

    public class ProductTypeDto 
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}