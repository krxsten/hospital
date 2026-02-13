using Hospital.Entities;
using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Diagnose
{
    public class DiagnoseViewModel
    {
        public Guid ID { get; set; }
        [StringLength(100, MinimumLength = 5)]
        [Required]
        public string Name { get; set; }
        public string Image { get; set; }
        public List<PatientAndDiagnose> ListOfPatientsAndDiagnoses { get; set; } = new List<PatientAndDiagnose>();
        public List<Hospital.Data.Entities.Medication> ListOfMedication { get; set; } = new List<Hospital.Data.Entities.Medication>();
    }
}
