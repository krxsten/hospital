using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.Specialization
{
    public class SpecializationCreateViewModel
    {
        public Guid ID { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string SpecializationName { get; set; }
    }
}