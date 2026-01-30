using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.WebProject.ViewModels.Medication
{
    public class MedicationIndexViewModel
    {
        public Guid ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [ForeignKey("Diagnose")]
        public Guid DiagnoseID { get; set; }
        public Hospital.Entities.Diagnose Diagnose { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
