using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.DTOs
{
    public class DoctorIndexDto
    {
        public Guid SpecializationId { get; set; }
        public string SpecializationName { get; set; } = null!;
        public Guid ShiftId { get; set; }
        public string ShiftName { get; set; } = null!;
        public Guid UserId { get; set; }
        public string UserName { get; set; } = null!;
        public bool IsAccepted { get; set; } = false;
        public string Image { get; set; } = null!;
        public List<Guid> PatientIDs { get; set; } = new();
        public List<(Guid DoctorID, Guid NurseID)> DoctorNurses { get; set; } = new();
        public List<Guid> CheckupIDs { get; set; } = new();
    }
}