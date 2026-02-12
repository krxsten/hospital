using Hospital.Data.Entities;
using Hospital.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Entities
{
    public class DoctorAndNurse
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        [ForeignKey("Doctor")]
        public Guid DoctorID { get; set; }
        public Doctor Doctor { get; set; }

        [Required]
        [ForeignKey("Nurse")]
        public Guid NurseID { get; set; }
        public Nurse Nurse { get; set; }
    }
}
