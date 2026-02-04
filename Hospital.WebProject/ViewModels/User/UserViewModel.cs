using Hospital.WebProject.ViewModels.Doctor;
using Hospital.WebProject.ViewModels.Nurse;
using Hospital.WebProject.ViewModels.Patient;
using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.User
{
    public class UserViewModel
    {
        public Guid UserID { get; set; }

        [Required, StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required, StringLength(100, MinimumLength = 2)]
        public string LastName { get; set; }

        public DoctorCreateViewModel Doctor { get; set; }
        public NurseViewModel Nurse { get; set; }
        public PatientViewModel Patient { get; set; }
    }
}
