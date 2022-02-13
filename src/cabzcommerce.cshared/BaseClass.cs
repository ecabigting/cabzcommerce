using System;
using System.ComponentModel.DataAnnotations;

namespace cabzcommerce.cshared
{
    public class BaseClass
    {
        [Required]
        public Guid Id { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public DateTimeOffset UpdatedDateTime { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        [Required]
        public bool IsEnabled {get;set;}
        [Required]
        public Guid IsEnabledBy { get; set; }

    }
}