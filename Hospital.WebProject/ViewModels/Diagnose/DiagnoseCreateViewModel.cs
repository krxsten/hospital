using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Diagnose
{
    public class DiagnoseCreateViewModel
    {
        [Required(ErrorMessage = "This field is required!")]
        public string Name { get; set; } = null!;
        public IFormFile? Image { get; set; }

    }
}
