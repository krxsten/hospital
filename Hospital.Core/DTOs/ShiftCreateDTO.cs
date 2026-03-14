using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.DTOs
{
    public class ShiftCreateDTO
    {
        public string Type { get; set; } = null!;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public List<Guid> ListOfDoctorsIDs { get; set; } = new();
        public List<string> ListOfDoctorsNames { get; set; } = new();
        public List<string> ListOfNursesNames { get; set; } = new();
        public List<Guid> ListOfNursesIDs { get; set; } = new();
    }
}
