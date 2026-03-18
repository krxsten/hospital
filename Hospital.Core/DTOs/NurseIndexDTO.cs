using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.DTOs
{
    public class NurseIndexDTO
    {
		public Guid ID { get; set; }
		public Guid UserID { get; set; }
        public string UserName { get; set; } = null!;
        public Guid SpecializationId { get; set; }
        public string SpecializationName { get; set; } = null!;
        public Guid ShiftId { get; set; }
        public string ShiftName { get; set; } = null!;
        public bool IsAccepted { get; set; }
        public string Image { get; set; } = null!;
        
    }
}
