using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Specialization
{
	public class SpecializationIndexViewModel
	{
		public Guid ID { get; set; }

		public string SpecializationName { get; set; } = null!;

        public string ImageURL { get; set; } = null!;
        public IFormFile NewImageFile { get; set; } = null!;
    }
}