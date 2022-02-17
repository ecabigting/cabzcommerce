using System.ComponentModel.DataAnnotations;

namespace cabzcommerce.cshared.Models 
{
    public class Brand : BaseClass
    {
       [Required]
       public string Name { get; set; }
       [Required]
       public string Description { get; set; }        
    }
}