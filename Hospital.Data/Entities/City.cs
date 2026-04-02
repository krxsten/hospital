using System;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Data.Entities
{
    public class City
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
