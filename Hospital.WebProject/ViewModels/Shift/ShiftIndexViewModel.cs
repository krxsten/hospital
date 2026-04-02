using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Shift
{
	public class ShiftIndexViewModel
	{
		public Guid ID { get; set; }

		[Required(ErrorMessage = "This field is required!")]
		public string Type { get; set; } = null!;

		[Required(ErrorMessage = "This field is required!")]
		public TimeOnly StartTime { get; set; }

		[Required(ErrorMessage = "This field is required!")]
		public TimeOnly EndTime { get; set; }
	}
}