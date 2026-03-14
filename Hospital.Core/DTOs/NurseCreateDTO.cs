using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.DTOs
{
    public class NurseCreateDTO
    {
        public Guid UserID { get; set; }
        public string UserName { get; set; } = null!;
        public Guid SpecializationId { get; set; }
        public string SpecializationName { get; set; } = null!;
        public Guid ShiftId { get; set; }
        public string ShiftName { get; set; } = null!;
        public bool IsAccepted { get; set; }
        public string Image { get; set; } = null!;
        public List<Guid> DoctorIDs { get; set; } = new();
        public List<string> DoctorNames { get; set; } = new();
    }
}
