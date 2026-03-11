using Hospital.Entities;
using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Diagnose
{
    public class DiagnoseViewModel
    {
        [Required]
        public Guid ID { get; set; }
        [StringLength(100, MinimumLength = 5)]
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        public List<PatientAndDiagnose> ListOfPatientsAndDiagnoses { get; set; } = new List<PatientAndDiagnose>();
        public List<Hospital.Data.Entities.Medication> ListOfMedication { get; set; } = new List<Hospital.Data.Entities.Medication>();
    }
}
