using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.DTOs
{
    public class DoctorShiftDTO
    {
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
