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
        public string NurseName { get; set; } = null!;
        public Guid SpecializationID { get; set; }
        public string SpecializationName { get; set; } = null!;
        public Guid ShiftID { get; set; }
        public bool IsAccepted { get; set; } = false;
        public IFormFile ImageFile { get; set; } = null!;

    }
}
