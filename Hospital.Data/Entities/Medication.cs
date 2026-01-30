using Hospital.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Data.Entities
{
    public class Medication
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [ForeignKey("Diagnose")]
        public Guid DiagnoseID { get; set; }
        public Diagnose Diagnose { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string SideEffects { get; set; }

    }
}
