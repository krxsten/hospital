using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Nurse
{
    public class NurseCreateViewModel
    {
        public Guid UserId { get; set; }

        public Hospital.Data.Entities.User User { get; set; }

        [Required]
        public Guid SpecializationId { get; set; }
        public Hospital.Entities.Specialization Specialization { get; set; }

        [Required]
        public Guid ShiftId { get; set; }
        public Hospital.Data.Entities.Shift Shift { get; set; }
        public bool IsAccepted { get; set; } = false;
    }
}
