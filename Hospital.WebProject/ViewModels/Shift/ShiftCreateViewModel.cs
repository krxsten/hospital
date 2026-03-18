using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Shift
{
	public class ShiftCreateViewModel
	{
		[Required(ErrorMessage = "This field is required!")]
		public string Type { get; set; } = null!;

		[Required(ErrorMessage = "This field is required!")]
		public TimeSpan StartTime { get; set; }

		[Required(ErrorMessage = "This field is required!")]
		public TimeSpan EndTime { get; set; }
	}
}
