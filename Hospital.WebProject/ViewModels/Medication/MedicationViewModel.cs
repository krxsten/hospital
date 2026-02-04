using Hospital.WebProject.ViewModels.Diagnose;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.WebProject.ViewModels.Medication
{
    public class MedicationViewModel
    {
        public Guid ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid DiagnoseID { get; set; }
        public Hospital.Entities.Diagnose Diagnose { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string SideEffects { get; set; }
    }
}
