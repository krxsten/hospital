using Hospital.WebProject.ViewModels.Diagnose;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.WebProject.ViewModels.Medication
{
    public class MedicationIndexViewModel
    {
		public Guid ID { get; set; }

		[Required(ErrorMessage = "This field is required!")]
		public string Name { get; set; } = null!;

		[Required(ErrorMessage = "This field is required!")]
		public Guid DiagnoseID { get; set; }

		public string DiagnoseName { get; set; } = null!;

		[Required(ErrorMessage = "This field is required!")]
		public string Description { get; set; } = null!;

		[Required(ErrorMessage = "This field is required!")]
		public string SideEffects { get; set; } = null!;
	}
}
