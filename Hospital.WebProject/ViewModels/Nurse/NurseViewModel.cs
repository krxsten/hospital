using Hospital.WebProject.ViewModels.Shift;
using Hospital.WebProject.ViewModels.Specialization;
using Hospital.WebProject.ViewModels.User;
using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Nurse
{
    public class NurseViewModel
    {
        public Guid UserID { get; set; }
        public Hospital.Data.Entities.User User { get; set; }

        [Required]
        public Guid SpecializationId { get; set; }
        public Hospital.Entities.Specialization Specialization { get; set; }

        [Required]
        public Guid ShiftId { get; set; }
        public Hospital.Data.Entities.Shift Shift { get; set; }
        public bool IsAccepted { get; set; } = false;
        public string Image { get; set; }
    }
}
