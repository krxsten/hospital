using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Shift
{
    public class ShiftIndexViewModel
    {
        public Guid ID { get; set; }
        public string Type { get; set; } = null!;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public List<Guid> ListOfDoctorsIDs { get; set; } = new();
        public List<string> ListOfDoctorsNames { get; set; } = new();
        public List<string> ListOfNursesNames { get; set; } = new();
        public List<Guid> ListOfNursesIDs { get; set; } = new();
    }
}