using Hospital.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.WebProject.ViewModels.Doctor
{
    public class DoctorCreateViewModel
    {
        public Guid SpecializationID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string SpecializationName { get; set; } = null!;

        public Guid ShiftID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string ShiftName { get; set; } = null!;

        public Guid UserID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string UserName { get; set; } = null!;
        [Required(ErrorMessage = "This field is required!")]
        public bool IsAccepted { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Image { get; set; } = null!;

        public List<Guid> PatientIDs { get; set; } = new();
        public List<string> PatientNames { get; set; } = new();

        public List<Guid> NurseIDs { get; set; } = new();
        public List<string> NurseNames { get; set; } = new();

        public List<Guid> CheckupIDs { get; set; } = new();
    }
}