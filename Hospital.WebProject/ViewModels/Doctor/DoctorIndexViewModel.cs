using Hospital.Data.Entities;
using Hospital.Entities;
using Hospital.WebProject.ViewModels.Shift;
using Hospital.WebProject.ViewModels.Specialization;
using Hospital.WebProject.ViewModels.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.WebProject.ViewModels.Doctor
{
    public class DoctorIndexViewModel
    {
        [Required]
        public Guid SpecializationId { get; set; }
        public Hospital.Entities.Specialization Specialization { get; set; }

        [Required]
        public Guid ShiftId { get; set; }
        public Hospital.Data.Entities.Shift Shift { get; set; }
        public Hospital.Data.Entities.User User { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public bool IsAccepted { get; set; } = false;
    }
}
