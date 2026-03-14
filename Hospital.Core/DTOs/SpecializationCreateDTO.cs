using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.DTOs
{
    public class SpecializationCreateDTO
    {
        public string SpecializationName { get; set; } = null!;
        public string Image { get; set; } = null!;
        public List<Guid> ListOfDoctors { get; set; } = new();
        public List<string> ListOfDoctorsNames { get; set; } = new();
        public List<Guid> ListOfNurses { get; set; } = new();
        public List<string> ListOfNursesNames { get; set; } = new();
    }
}
