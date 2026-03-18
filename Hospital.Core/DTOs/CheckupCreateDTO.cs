using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.DTOs
{
    public class CheckupCreateDTO
    {
        public Guid PatientID { get; set; }
        public Guid DoctorID { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
    }
}
