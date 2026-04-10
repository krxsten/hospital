using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Nurse
{
    public class NurseEditViewModel
    {
        public Guid ID { get; set; }
        [Required]
        public string NurseName { get; set; } = null!;

        [Required(ErrorMessage = "This field is required!")]
        public Guid SpecializationId { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public Guid ShiftId { get; set; }

        public bool IsAccepted { get; set; }

        public string? ExistingImage { get; set; }

        public IFormFile? File { get; set; }
    }
}
