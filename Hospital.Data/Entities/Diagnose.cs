using Hospital.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Entities
{
    public class Diagnose
    {
        [Key]
        public Guid ID { get; set; }
        [StringLength(100, MinimumLength = 5)]
        [Required]
        public string Name { get; set; }
        public string Image { get; set; }
        public List<PatientAndDiagnose> ListOfPatientsAndDiagnoses { get; set; } = new List<PatientAndDiagnose>();
        public List<Medication> ListOfMedication { get; set; } = new List<Medication>();

    }
}
    