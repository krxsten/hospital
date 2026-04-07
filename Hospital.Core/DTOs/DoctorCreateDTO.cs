using Microsoft.AspNetCore.Http;

namespace Hospital.Core.DTOs
{
    public class DoctorCreateDto
    {
        public string DoctorName { get; set; } = null!;
        public Guid SpecializationID { get; set; }
        public string SpecializationName { get; set; } = null!;
        public Guid ShiftID { get; set; }
        public bool IsAccepted { get; set; } = false;
        public IFormFile ImageFile { get; set; } = null!;
    }
}