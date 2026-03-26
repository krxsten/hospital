using Hospital.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.WebProject.ViewModels.Doctor
{
	public class DoctorCreateViewModel
	{
		public Guid ID { get; set; }

		[Required(ErrorMessage = "This field is required!")]
		public Guid SpecializationID { get; set; }

		[Required(ErrorMessage = "This field is required!")]
		public Guid ShiftID { get; set; }

		[Required(ErrorMessage = "This field is required!")]
		public Guid UserID { get; set; }

		public bool IsAccepted { get; set; }

		[Required(ErrorMessage = "This field is required!")]
        public IFormFile File { get; set; } = null!;
    }
}