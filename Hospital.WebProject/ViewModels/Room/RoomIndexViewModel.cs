using Hospital.Entities;
using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Room
{
    public class RoomIndexViewModel
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public int RoomNumber { get; set; }
        [Required]
        public bool IsTaken { get; set; }
        public int PatientsCount { get; set; }
    }
}
