using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Specialization
{
	public class SpecializationIndexViewModel
	{
		public Guid ID { get; set; }

		[Required(ErrorMessage = "This field is required!")]
		public string SpecializationName { get; set; } = null!;

		[Required(ErrorMessage = "This field is required!")]
		public string Image { get; set; } = null!;
	}
}