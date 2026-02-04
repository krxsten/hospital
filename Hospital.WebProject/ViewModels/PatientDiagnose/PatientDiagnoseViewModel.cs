using Hospital.WebProject.ViewModels.Patient;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.WebProject.ViewModels.PatientDiagnose
{
    public class PatientDiagnoseViewModel
    {
        [Required]
        [ForeignKey("Patient")]
        public Guid PatientId { get; set; }
        public Hospital.Data.Entities.Patient Patient { get; set; }

        [Required]
        [ForeignKey("Diagnose")]
        public Guid DiagnoseId { get; set; }
        public Hospital.Entities.Diagnose Diagnose { get; set; }
    }
}
