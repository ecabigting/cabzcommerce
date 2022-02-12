using System.ComponentModel.DataAnnotations;

namespace cabzcommerce.cshared.Models 
{
    public class Product : BaseClass
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
        public int DiscountPercentage { get; set; }
        [Required]
        public int MyProperty { get; set; }
        [Required]
        public List<string> Type { get; set; }
        [Required]
        public Guid BrandID { get; set; }
        [Required]
        public string BrandName {get;set;}
        [Required]
        public bool InStock { get; set; }
        [Required]
        public string SKUCode { get; set; }
        [Required]
        public string Barcode { get; set; }
        public string InventoryCode { get; set; }        
        public List<ProductCustomDescription> DescriptionList {get;set;}
        public Guid ShopId { get; set; }
        
    }
    public class ProductCustomDescription :BaseClass
    {
        public string DescriptionTitle { get; set; }
        public string DescriptionText { get; set; }

    }

}