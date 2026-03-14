using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Nurse
{
    public class NurseCreateViewModel
    {
        public Guid UserID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string UserName { get; set; } = null!;

        public Guid SpecializationId { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string SpecializationName { get; set; } = null!;

        public Guid ShiftId { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string ShiftName { get; set; } = null!;
        [Required(ErrorMessage = "This field is required!")]
        public bool IsAccepted { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Image { get; set; } = null!;

        public List<Guid> DoctorIDs { get; set; } = new();
        public List<string> DoctorNames { get; set; } = new();
    }
}
