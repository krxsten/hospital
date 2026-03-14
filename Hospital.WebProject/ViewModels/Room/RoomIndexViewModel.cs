using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Room
{
    public class RoomIndexViewModel
    {
        public Guid ID { get; set; }
        public int RoomNumber { get; set; }
        public bool IsTaken { get; set; }
        public List<Guid> ListOfPatientsIDs { get; set; } = new();
        public List<string> ListOfPatientsNames { get; set; } = new();
    }
}
