using Hospital.Entities;
using Hospital.WebProject.ViewModels.Doctor;
using Hospital.WebProject.ViewModels.Room;
using Hospital.WebProject.ViewModels.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.WebProject.ViewModels.Patient
{
    public class PatientIndexViewModel
    {
        public Guid UserID { get; set; }
        public string UserName { get; set; } = null!;
        public Guid DoctorId { get; set; }
        public string DoctorName { get; set; } = null!;
        public DateTime HospitalizationDate { get; set; }
        public DateTime DischargeDate { get; set; }

        public Guid RoomId { get; set; }
        public int RoomNumber { get; set; }
        [StringLength(100, MinimumLength = 2)]
        public string BirthCity { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        [Phone(ErrorMessage = "Invalid phone number!")]
        public string PhoneNumber { get; set; } = null!;
        [StringLength(10)]
        public string UCN { get; set; } = null!;
        public List<Guid> PatientDiagnosesIDs { get; set; } = new();
        public List<string> PatientDiagnosesName { get; set; } = new();

        public List<Guid> CheckupIDs { get; set; } = new();
        public List<string> Checkups { get; set; } = new();
    }
}

