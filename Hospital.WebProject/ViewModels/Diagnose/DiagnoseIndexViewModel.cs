using Hospital.Entities;
using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Diagnose
{
    public class DiagnoseIndexViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;

        public List<Guid> PatientIDs { get; set; } = new();
        public List<string> PatientNames { get; set; } = new();

        public List<Guid> MedicationIDs { get; set; } = new();
        public List<string> MedicationNames { get; set; } = new();
    }
}