using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Shift
{
    public class ShiftIndexViewModel
    {
        public Guid ID { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public DateTime StartTime { get; set; } = new DateTime();
        [Required]
        public DateTime EndTime { get; set; } = new DateTime();
    }
}
