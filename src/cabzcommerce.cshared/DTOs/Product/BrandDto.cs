using System.ComponentModel.DataAnnotations;

namespace cabzcommerce.cshared.DTOs.Product
{
    public class BrandDto
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public bool Enabled {get;set;}
    }
}