using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.DTOs
{
    public class SpecializationIndexDTO
    {
        public Guid ID { get; set; }
        public string SpecializationName { get; set; } = null!;
        public string Image { get; set; } = null!;
    }
}
