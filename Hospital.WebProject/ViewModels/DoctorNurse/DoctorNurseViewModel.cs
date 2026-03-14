using Hospital.WebProject.ViewModels.Doctor;
using Hospital.WebProject.ViewModels.Nurse;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.WebProject.ViewModels.DoctorNurse
{
    public class DoctorNurseViewModel
    {
        public Guid DoctorID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string DoctorName { get; set; } = null!;

        public Guid NurseID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string NurseName { get; set; } = null!;
    }
}

