using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.WebProject.ViewModels.Doctor
{
    public class DoctorCreateViewModel
    {
        public Guid SpecializationID { get; set; }
        public Hospital.Entities.Specialization Specialization { get; set; }
        public Guid ShiftID { get; set; }
        public Hospital.Data.Entities.Shift Shift { get; set; }
        [Required]
        public Guid UserID { get; set; }
        public Hospital.Data.Entities.User User { get; set; }
        public bool IsAccepted { get; set; } = false;
    }
}