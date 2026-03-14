using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Specialization
{
    public class SpecializationCreateViewModel
    {
        [StringLength(100, MinimumLength = 5)]
        [Required(ErrorMessage = "This field is required!")]
        public string SpecializationName { get; set; } = null!;
        [Required(ErrorMessage = "This field is required!")]
        public string Image { get; set; } = null!;
        public List<Guid> ListOfDoctors { get; set; } = new();
        public List<string> ListOfDoctorsNames { get; set; } = new();
        public List<Guid> ListOfNurses { get; set; } = new();
        public List<string> ListOfNursesNames { get; set; } = new();
    }
}
