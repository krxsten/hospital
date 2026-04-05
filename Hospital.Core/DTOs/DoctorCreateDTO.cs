using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.DTOs
{
    public class DoctorCreateDto
    {
        public Guid SpecializationID { get; set; }
        public string SpecializationName { get; set; } = null!;
        public Guid ShiftID { get; set; }
        public Guid UserID { get; set; }
        public bool IsAccepted { get; set; } = false;
        public string ImageURL { get; set; }
        public string CloudinaryID { get; set; }

    }
}
