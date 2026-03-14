using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Medication
{
    public class MedicationCreateViewModel
    {
        [Required(ErrorMessage = "This field is required!")]
        public string Name { get; set; } = null!;
        public Guid DiagnoseID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string DiagnoseName { get; set; } = null!;
        [Required(ErrorMessage = "This field is required!")]
        public string Description { get; set; } = null!;
        [Required(ErrorMessage = "This field is required!")]
        public string SideEffects { get; set; } = null!;
    }
}
