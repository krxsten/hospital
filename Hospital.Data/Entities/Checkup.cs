using Hospital.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Data.Entities
{
    public class Checkup
    {
        public Guid ID { get; set; }
        [ForeignKey("Patient")]
        public Guid PatientID { get; set; }
        public Patient Patient { get; set; }
        [ForeignKey("Doctor")]
        public Guid DoctorID { get; set; }
        public Doctor Doctor { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

    }
}
