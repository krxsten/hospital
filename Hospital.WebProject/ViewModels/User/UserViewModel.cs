using Hospital.Data.Entities;
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

        public Hospital.Entities.Doctor Doctor { get; set; }
        public Hospital.Entities.Nurse Nurse { get; set; }
        public Hospital.Data.Entities.Patient Patient { get; set; }
    }
}
