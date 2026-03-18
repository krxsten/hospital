using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.DTOs
{
    public class PatientCreateDTO
    {
        public Guid UserID { get; set; }
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
