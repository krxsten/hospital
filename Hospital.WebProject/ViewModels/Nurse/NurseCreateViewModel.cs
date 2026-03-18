using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Nurse
{
	public class NurseCreateViewModel
	{
		public Guid ID { get; set; }

		[Required(ErrorMessage = "This field is required!")]
		public Guid UserID { get; set; }

		[Required(ErrorMessage = "This field is required!")]
		public Guid SpecializationId { get; set; }

		[Required(ErrorMessage = "This field is required!")]
		public Guid ShiftId { get; set; }

		public bool IsAccepted { get; set; }

		[Required(ErrorMessage = "This field is required!")]
		public string Image { get; set; } = null!;
	}
}
