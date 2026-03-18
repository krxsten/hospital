using Hospital.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Data.Entities
{
    public class Patient
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; }
        [Required]
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        [DataType(DataType.Date)]
        public DateOnly HospitalizationDate { get; set; }
        [DataType(DataType.Time)]
        public TimeOnly HospitalizationTime { get; set; }
        [DataType(DataType.Date)]
        public DateOnly DischargeDate { get; set; }
        [DataType(DataType.Time)]
        public TimeOnly DischargeTime { get; set; }

        [Required]
        public Guid RoomId { get; set; }
        public Room Room { get; set; }

        [StringLength(100, MinimumLength = 2)]
        public string BirthCity { get; set; }

        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }
        

        [Required]
        [Phone(ErrorMessage = "Невалиден телефон")]
        public string PhoneNumber { get; set; }
            
        [StringLength(10)]
        public string UCN { get; set; }

        public List<PatientAndDiagnose> PatientDiagnoses { get; set; } = new List<PatientAndDiagnose>();

        public List<Checkup> Checkups { get; set; } = new List<Checkup>();
    }
}
