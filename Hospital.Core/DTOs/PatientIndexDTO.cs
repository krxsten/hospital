using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.DTOs
{
    public class PatientIndexDTO
    {
        public Guid UserID { get; set; }
        public string UserName { get; set; } = null!;
        public Guid DoctorId { get; set; }
        public string DoctorName { get; set; } = null!;
        public DateTime HospitalizationDate { get; set; }
        public DateTime DischargeDate { get; set; }
        public Guid RoomId { get; set; }
        public int RoomNumber { get; set; }
        public string BirthCity { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string UCN { get; set; } = null!;
        public List<Guid> PatientDiagnosesIDs { get; set; } = new();
        public List<string> PatientDiagnosesName { get; set; } = new();
        public List<Guid> CheckupIDs { get; set; } = new();
        public List<string> Checkups { get; set; } = new();
    }
}
