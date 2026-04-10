using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.DTOs
{
    public class NurseEditDTO
    {
        public Guid ID { get; set; }
        public string NurseName { get; set; } = null!;
        public Guid SpecializationId { get; set; }
        public Guid ShiftId { get; set; }
        public bool IsAccepted { get; set; }
        public string ImageURL { get; set; } = null!;
        public IFormFile? NewImageFile { get; set; }
    }
}
