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
		public Guid ID { get; set; }

		public Guid? UserID { get; set; }
		public string UserName { get; set; } = null!;

		public Guid? DoctorId { get; set; }
		public string DoctorName { get; set; } = null!;

        public DateOnly HospitalizationDate { get; set; }
        public TimeOnly HospitalizationTime { get; set; }
        public DateOnly DischargeDate { get; set; }
        public TimeOnly DischargeTime { get; set; }

        public Guid? RoomId { get; set; }
		public int RoomNumber { get; set; }

		[StringLength(100, MinimumLength = 2)]
		public string BirthCity { get; set; } = null!;

        public DateOnly DateOfBirth { get; set; }

        [Phone(ErrorMessage = "Invalid phone number!")]
		public string PhoneNumber { get; set; } = null!;

		[StringLength(10)]
		public string UCN { get; set; } = null!;

	}
}

