using Hospital.Data.Entities;
using Microsoft.AspNetCore.Http;
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
        [Required]
        public string ImageURL { get; set; } = null!;
        [Required]
        public string PublicID { get; set; } = null!;
        public List<PatientAndDiagnose> ListOfPatientsAndDiagnoses { get; set; } = new List<PatientAndDiagnose>();
        public List<Medication> ListOfMedication { get; set; } = new List<Medication>();

    }
}
    