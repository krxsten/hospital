using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Specialization
{
    public class SpecializationViewModel
    {
        public Guid ID { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string SpecializationName { get; set; }
        public string Image { get; set; }
        public List<Hospital.Entities.Doctor> ListOfDoctors { get; set; } = new List<Hospital.Entities.Doctor>();
        public List<Hospital.Entities.Nurse> ListOfNurses { get; set; } = new List<Hospital.Entities.Nurse>();
    }
}