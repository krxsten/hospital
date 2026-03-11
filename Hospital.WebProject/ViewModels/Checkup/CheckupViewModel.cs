using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.WebProject.ViewModels.Checkup
{
    public class CheckupViewModel
    {
        [Required]
        public Guid ID { get; set; }
        [Required]
        public Guid PatientID { get; set; }
        public Hospital.Data.Entities.Patient Patient { get; set; }
        [Required]
        public Guid DoctorID { get; set; }
        public Hospital.Entities.Doctor Doctor { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime Date { get; set; }
    }
}
