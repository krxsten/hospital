using Hospital.Entities;
using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Patient
{
    public class PatientCreateViewModel
    {
        public Guid UserID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string UserName { get; set; } = null!;
        public Guid DoctorId { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string DoctorName { get; set; } = null!;
        [Required(ErrorMessage = "This field is required!")]
        public DateTime HospitalizationDate { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public DateTime DischargeDate { get; set; }

        public Guid RoomId { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public int RoomNumber { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [StringLength(100, MinimumLength = 2)]
        public string BirthCity { get; set; } = null!;
        [Required(ErrorMessage = "This field is required!")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [Phone(ErrorMessage = "Invalid phone number!")]
        public string PhoneNumber { get; set; } = null!;
        [Required(ErrorMessage = "This field is required!")]
        [StringLength(10)]
        public string UCN { get; set; } = null!;
        public List<Guid> PatientDiagnosesIDs { get; set; } = new();
        public List<string> PatientDiagnosesName { get; set; } = new();

        public List<Guid> CheckupIDs { get; set; } = new();
        public List<string> Checkups { get; set; } = new();

    }
}
