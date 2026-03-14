using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.DTOs
{
    public class DoctorCreateDto
    {
        public Guid SpecializationID { get; set; }
        public string SpecializationName { get; set; } = null!;
        public Guid ShiftID { get; set; }
        public Guid UserID { get; set; }
        public bool IsAccepted { get; set; } = false;
        public string Image { get; set; } = null!;
        public List<Guid> PatientIDs { get; set; } = new();
        public List<(Guid DoctorID, Guid NurseID)> DoctorNurses { get; set; } = new();
        public List<Guid> CheckupIDs { get; set; } = new();
    }
}
