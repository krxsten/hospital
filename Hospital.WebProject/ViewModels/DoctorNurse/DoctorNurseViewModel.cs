using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.WebProject.ViewModels.DoctorNurse
{
    public class DoctorNurseViewModel
    {
        [Required]
        public Guid DoctorID { get; set; }
        public Hospital.Entities.Doctor Doctor { get; set; }

        [Required]
        public Guid NurseID { get; set; }
        public Hospital.Entities.Nurse Nurse { get; set; }
        [Required]
        public int CountOfAssignedPatients { get; set; }
    }
}
