using Hospital.Entities;
using Hospital.WebProject.ViewModels.Shift;
using Hospital.WebProject.ViewModels.Specialization;
using Hospital.WebProject.ViewModels.User;
using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Nurse
{
	public class NurseIndexViewModel
	{
		public Guid ID { get; set; }
		public Guid UserID { get; set; }
		public string UserName { get; set; } = null!;
		public Guid SpecializationId { get; set; }
		public string SpecializationName { get; set; } = null!;
		public Guid ShiftId { get; set; }
		public string ShiftName { get; set; } = null!;
		public bool IsAccepted { get; set; }
        public string ImageURL { get; set; } = null!;
    }
}

