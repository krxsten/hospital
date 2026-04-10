using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.DTOs
{
    public class PatientEditDTO
    {
        public Guid ID { get; set; }
        public string PatientName { get; set; } = null!;
        public Guid DoctorId { get; set; } 
        public DateOnly HospitalizationDate { get; set; }
        public TimeOnly HospitalizationTime { get; set; }
        public DateOnly DischargeDate { get; set; }
        public TimeOnly DischargeTime { get; set; }
        public Guid RoomId { get; set; }
        public string BirthCity { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string UCN { get; set; } = null!;
    }
}
