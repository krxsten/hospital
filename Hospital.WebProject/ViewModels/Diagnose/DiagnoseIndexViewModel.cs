using Hospital.Entities;
using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Diagnose
{
	public class DiagnoseIndexViewModel
	{
		public Guid ID { get; set; }

		[Required(ErrorMessage = "This field is required!")]
		public string Name { get; set; } = null!;

		[Required(ErrorMessage = "This field is required!")]
		public string Image { get; set; } = null!;
	}
}