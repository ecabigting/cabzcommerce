using System.ComponentModel.DataAnnotations;

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
        public string ProductImageDesktopUrl { get; set; }
        [Required]
        public string ProductImageMobileUrl { get; set; }
        [Required]
        public List<string> Type { get; set; }
        [Required]
        public Guid BrandID { get; set; }
        [Required]
        public string BrandName {get;set;}

    }
}