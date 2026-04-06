using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Specialization
{
	public class SpecializationCreateViewModel
	{
		[Required(ErrorMessage = "This field is required!")]
		public string SpecializationName { get; set; } = null!;

		[Required(ErrorMessage = "This field is required!")]
        public IFormFile? Image { get; set; }
    }
}
