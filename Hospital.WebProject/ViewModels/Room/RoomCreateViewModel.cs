using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Room
{
    public class RoomCreateViewModel
    {
        [Required(ErrorMessage = "This field is required!")]
        public int RoomNumber { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public bool IsTaken { get; set; }
        public List<Guid> ListOfPatientsIDs { get; set; } = new();
        public List<string> ListOfPatientsNames { get; set; } = new();
    }
}
