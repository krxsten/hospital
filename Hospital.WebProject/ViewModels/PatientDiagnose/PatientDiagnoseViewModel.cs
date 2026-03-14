using Hospital.WebProject.ViewModels.Patient;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.WebProject.ViewModels.PatientDiagnose
{
    public class PatientDiagnoseViewModel
    {
        public Guid PatientId { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string PatientName { get; set; } = null!;

        public Guid DiagnoseId { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string DiagnoseName { get; set; } = null!;
    }
}
