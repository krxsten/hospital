using Hospital.WebProject.ViewModels.Diagnose;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.WebProject.ViewModels.Medication
{
    public class MedicationIndexViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; } = null!;
        public Guid DiagnoseID { get; set; }
        public string DiagnoseName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string SideEffects { get; set; } = null!;
    }
}
