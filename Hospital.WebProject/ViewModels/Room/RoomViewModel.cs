using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Room
{
    public class RoomViewModel
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public int RoomNumber { get; set; }
        [Required]
        public bool IsTaken { get; set; }
    }
}
