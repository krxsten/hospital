using Hospital.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Entities
{
    public class PatientAndDiagnose
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("Patient")]
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }

        [Required]
        [ForeignKey("Diagnose")]
        public Guid DiagnoseId { get; set; }
        public Diagnose Diagnose { get; set; }
    }
}
