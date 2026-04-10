using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Nurse
{
	public class NurseCreateViewModel
	{
        [Required(ErrorMessage = "This field is required!")]
        public string NurseName { get; set; } = null!;

        [Required(ErrorMessage = "This field is required!")]
        public Guid SpecializationID { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public Guid ShiftID { get; set; }

        public bool IsAccepted { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public IFormFile? Image { get; set; }
    }
}
