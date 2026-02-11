using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.WebProject.ViewModels.Checkup
{
    public class CheckupViewModel
    {
        public Guid ID { get; set; }
        public Guid PatientID { get; set; }
        public Hospital.Data.Entities.Patient Patient { get; set; }
        public Guid DoctorID { get; set; }
        public Hospital.Entities.Doctor Doctor { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
