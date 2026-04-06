using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.DTOs
{
    public class NurseCreateDTO
    {
        public Guid UserID { get; set; }
        public Guid SpecializationId { get; set; }
        public Guid ShiftId { get; set; }
        public bool IsAccepted { get; set; }
        public IFormFile ImageFile { get; set; } = null!;

    }
}
