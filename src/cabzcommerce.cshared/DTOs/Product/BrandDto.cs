using System.ComponentModel.DataAnnotations;

namespace cabzcommerce.cshared.DTOs.Product
{
    public class BrandDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Desc { get; set; }
        [Required]
        public bool Enabled {get;set;}
    }
}