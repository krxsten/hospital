using Hospital.WebProject.ViewModels.Doctor;
using Hospital.WebProject.ViewModels.Room;
using Hospital.WebProject.ViewModels.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.WebProject.ViewModels.Patient
{
    public class PatientViewModel
    {
        public Guid UserID { get; set; }
        public Hospital.Data.Entities.User User { get; set; }
        [Required]
        public Guid DoctorId { get; set; }
        public Hospital.Entities.Doctor Doctor { get; set; }

        public DateTime HospitalizationDate { get; set; }
        public DateTime DischargeDate { get; set; }

        [Required]
        public Guid RoomId { get; set; }
        public Hospital.Entities.Room Room { get; set; }

        [StringLength(100, MinimumLength = 2)]
        public string BirthCity { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required]
        [Phone(ErrorMessage = "Невалиден телефон")]
        public string PhoneNumber { get; set; }

        [StringLength(10)]
        public string UCN { get; set; }
    }
}
