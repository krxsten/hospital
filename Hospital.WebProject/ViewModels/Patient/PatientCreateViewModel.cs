using Hospital.Entities;
using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Patient
{
	public class PatientCreateViewModel
	{
        [Required(ErrorMessage = "This field is required!")]
        public string PatientName { get; set; } = null!;

        [Required(ErrorMessage = "This field is required!")]
		public Guid DoctorId { get; set; }

		[Required(ErrorMessage = "This field is required!")]
		public Guid RoomId { get; set; }

		[Required(ErrorMessage = "This field is required!")]
        [DataType(DataType.Date)]
        public DateOnly HospitalizationDate { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [DataType(DataType.Time)]
        public TimeOnly HospitalizationTime { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [DataType(DataType.Date)]
        public DateOnly DischargeDate { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [DataType(DataType.Time)]
        public TimeOnly DischargeTime { get; set; }

        [Required(ErrorMessage = "This field is required!")]
		public string BirthCity { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "This field is required!")]
		[Phone(ErrorMessage = "Invalid phone number!")]
        [StringLength(10)]
		public string PhoneNumber { get; set; } = null!;

		[Required(ErrorMessage = "This field is required!")]
		[StringLength(10)]
		public string UCN { get; set; } = null!;
	}
}
