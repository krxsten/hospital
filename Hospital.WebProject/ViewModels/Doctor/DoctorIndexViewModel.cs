using Hospital.Data.Entities;
using Hospital.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.WebProject.ViewModels.Doctor
{
    public class DoctorIndexViewModel
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
