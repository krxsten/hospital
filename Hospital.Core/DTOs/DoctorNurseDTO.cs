using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.DTOs
{
    public class DoctorNurseDto
    {
        public Guid DoctorID { get; set; }
        public Guid NurseID { get; set; }
    }
}
