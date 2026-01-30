using System.ComponentModel.DataAnnotations;

namespace Hospital.WebProject.ViewModels.User
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(40, MinimumLength = 5)]
        public string FirstName { get; set; } = null!;
        [Required]
        [StringLength(40, MinimumLength = 5)]
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
    }
}
