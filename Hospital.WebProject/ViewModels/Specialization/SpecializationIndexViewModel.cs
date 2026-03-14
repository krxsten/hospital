using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Specialization
{
    public class SpecializationIndexViewModel
    {
        public Guid ID { get; set; }
        [StringLength(100, MinimumLength = 5)]
        public string SpecializationName { get; set; } = null!;
        public string Image { get; set; } = null!;
        public List<Guid> ListOfDoctors { get; set; } = new();
        public List<string> ListOfDoctorsNames { get; set; } = new();
        public List<Guid> ListOfNurses { get; set; } = new();
        public List<string> ListOfNursesNames { get; set; } = new();
    }
}