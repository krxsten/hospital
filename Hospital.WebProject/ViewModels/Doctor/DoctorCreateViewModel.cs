using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Doctor
{
    public class DoctorCreateViewModel
    {
        [Required(ErrorMessage = "This field is required!")]
        public string DoctorName { get; set; } = null!;

        [Required(ErrorMessage = "This field is required!")]
        public Guid SpecializationID { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public Guid ShiftID { get; set; }

        public bool IsAccepted { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public IFormFile? Image { get; set; }
    }
}