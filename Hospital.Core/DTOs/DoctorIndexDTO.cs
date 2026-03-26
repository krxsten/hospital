using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.DTOs
{
    public class DoctorIndexDto
    {
		public Guid ID { get; set; }
		public Guid SpecializationId { get; set; }
        public string SpecializationName { get; set; } = null!;
        public Guid ShiftId { get; set; }
        public string ShiftName { get; set; } = null!;
        public Guid UserId { get; set; }
        public string UserName { get; set; } = null!;
        public bool IsAccepted { get; set; } = false;
        public string ImageURL { get; set; } = null!;
        
    }
}