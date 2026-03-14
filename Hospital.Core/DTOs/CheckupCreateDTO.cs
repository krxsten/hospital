using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.DTOs
{
    public class CheckupCreateDTO
    {
        public Guid PatientID { get; set; }
        public Guid DoctorID { get; set; }
        public DateTime Date { get; set; }
    }
}
