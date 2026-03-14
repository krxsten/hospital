using Hospital.Data.Entities;
using Hospital.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.DTOs
{
    public class UserDTO
    {
        public Guid UserID { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = string.Empty!;
        public Doctor Doctor { get; set; }
        public Nurse Nurse { get; set; }
        public Patient Patient { get; set; }
    }
}
