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
        public List<ProductType> Type { get; set; }
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
        public Guid ShopId { get; set; }
        
    }
    public class ProductCustomDescription :BaseClass
    {
        public string DescriptionTitle { get; set; }
        public string DescriptionText { get; set; }
        [Required]
        public Guid ProductId { get;set; }
    }

    public class ProductType : BaseClass 
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }

    public class ProductAddOns : BaseClass
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Desc { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string ImgDesktopUrl { get; set; }        
        public string ImgMobileUrl { get; set; }
        public bool InStock { get; set; }

    }

}