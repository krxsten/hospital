using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Checkup
{
    public class CheckupCreateViewModel
    {
        [Required(ErrorMessage ="This field is required!")]
        public Guid PatientID { get; set; }
        public string PatientName { get; set; } = null!;
        [Required(ErrorMessage = "This field is required!")]
        public Guid DoctorID { get; set; }
        public string DoctorName { get; set; } = null!;
        [Required(ErrorMessage = "This field is required!")]
        public DateTime Date { get; set; }
    }
}
