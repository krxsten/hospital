using Microsoft.AspNetCore.Http;

namespace Hospital.WebProject.ViewModels.Doctor
{
    public class DoctorIndexViewModel
    {
		public Guid ID { get; set; }

		public Guid SpecializationId { get; set; }
		public string SpecializationName { get; set; } = null!;

		public Guid ShiftId { get; set; }
		public string ShiftName { get; set; } = null!;

		public Guid UserId { get; set; }
		public string UserName { get; set; } = null!;

		public bool IsAccepted { get; set; }

        public string ImageURL { get; set; } = null!;
    }
}