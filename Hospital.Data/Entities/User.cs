using Hospital.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Data.Entities;

public class User : IdentityUser<Guid>
{
    [Required, StringLength(100, MinimumLength = 2)]
    public string FirstName { get; set; }

    [Required, StringLength(100, MinimumLength = 2)]
    public string LastName { get; set; }

    public Doctor Doctor { get; set; }
    public Nurse Nurse { get; set; }
    public Patient Patient { get; set; }
}
