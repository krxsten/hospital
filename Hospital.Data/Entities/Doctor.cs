using Hospital.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Entities
{
    public class Doctor
    {
        [Key]
        public Guid UserId { get; set; }

        public User User { get; set; }

        [Required]
        public Guid SpecializationId { get; set; }
        public Specialization Specialization { get; set; }

        [Required]
        public Guid ShiftId { get; set; }
        public Shift Shift { get; set; }

        public bool IsAccepted { get; set; } = false;
        public List<Patient> Patients { get; set; } = new List<Patient>();
        public List<DoctorAndNurse> DoctorNurses { get; set; } = new List<DoctorAndNurse>();
    }
}
