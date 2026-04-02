using System.ComponentModel.DataAnnotations;
namespace Hospital.WebProject.ViewModels.User
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; } = null!;
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string LastName { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
        [Required]
        public string Role { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid? SpecializationId { get; set; }
        public Guid? ShiftId { get; set; }
        public string? ImageURL { get; set; }
        public string BirthCity { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string UCN { get; set; }
    }
}
