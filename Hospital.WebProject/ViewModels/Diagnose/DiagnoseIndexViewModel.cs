using Hospital.Entities;
using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Diagnose
{
	public class DiagnoseIndexViewModel
	{
		public Guid ID { get; set; }

		public string Name { get; set; } = null!;

		public string ImageURL { get; set; } = null!;
		public IFormFile NewImageFile { get; set; } = null!;
	}
}