using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Room
{
    public class RoomCreateViewModel
    {
		[Required(ErrorMessage = "This field is required!")]
		public int RoomNumber { get; set; }

		public bool IsTaken { get; set; }
	}
}
