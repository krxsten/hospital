using Hospital.Data.Entities;
using Hospital.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.DTOs
{
    public class CheckupIndexDTO
    {
        public Guid ID { get; set; }
        public Guid PatientID { get; set; } 
        public Guid DoctorID { get; set; }
        public DateTime Date { get; set; } 
    }
}
