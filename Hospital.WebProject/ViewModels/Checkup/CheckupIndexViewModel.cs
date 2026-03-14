using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.WebProject.ViewModels.Checkup
{
    public class CheckupIndexViewModel
    {
        public Guid ID { get; set; }
        public Guid PatientID { get; set; }
        public string PatientName { get; set; } = null!;
        public Guid DoctorID { get; set; }
        public string DoctorName { get; set; } = null!;
        public DateTime Date { get; set; }
    }
}
