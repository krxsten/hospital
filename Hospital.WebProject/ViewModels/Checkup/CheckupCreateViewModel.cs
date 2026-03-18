using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Checkup
{
    public class CheckupCreateViewModel
    {
        [Required(ErrorMessage ="This field is required!")]
        public Guid PatientID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public Guid DoctorID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [DataType(DataType.Date)]
        public DateOnly Date { get; set; }
        [DataType(DataType.Time)]
        public TimeOnly Time { get; set; }
    }
}
