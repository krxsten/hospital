using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Patient
{
	public class SelectDoctorAndRoomViewModel
	{
		[Required(ErrorMessage = "Please select a doctor!")]
		[Display(Name = "Doctor")]
		public Guid DoctorId { get; set; }

		[Required(ErrorMessage = "Please select a room!")]
		[Display(Name = "Room")]
		public Guid RoomId { get; set; }

		[Required(ErrorMessage = "Birth city is required!")]
		[StringLength(100, MinimumLength = 2)]
		[Display(Name = "Birth City")]
		public string BirthCity { get; set; } = null!;

		[Required(ErrorMessage = "Date of birth is required!")]
		[DataType(DataType.Date)]
		[Display(Name = "Date of Birth")]
		public DateOnly DateOfBirth { get; set; }

		[Required(ErrorMessage = "Phone number is required!")]
		[Phone(ErrorMessage = "Invalid phone number!")]
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; } = null!;

		[Required(ErrorMessage = "UCN is required!")]
		[StringLength(10)]
		[Display(Name = "UCN")]
		public string UCN { get; set; } = null!;
	}
}
