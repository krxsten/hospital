using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.DTOs
{
    public class PatientDiagnoseDTO
    {
        public Guid PatientId { get; set; }
        public string PatientName { get; set; } = null!;

        public Guid DiagnoseId { get; set; }
        public string DiagnoseName { get; set; } = null!;
    }
}
