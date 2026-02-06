using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Diagnose
{
    public class DiagnoseViewModel
    {
        public Guid ID { get; set; }
        [StringLength(100, MinimumLength = 5)]
        [Required]
        public string Name { get; set; }

    }
}
