using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Shift
{
    public class ShiftViewModel
    {
        public Guid ID { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public List<Hospital.Entities.Doctor> ListOfDoctors { get; set; } = new List<Hospital.Entities.Doctor>();
        public List<Hospital.Entities.Nurse> ListOfNurses { get; set; } = new List<Hospital.Entities.Nurse>();
    }
}