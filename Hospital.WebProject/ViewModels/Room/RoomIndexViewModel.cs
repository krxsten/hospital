using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Room
{
    public class RoomIndexViewModel
    {
		public Guid ID { get; set; }

		[Required(ErrorMessage = "This field is required!")]
		public int RoomNumber { get; set; }

		public bool IsTaken { get; set; }
	}
}
