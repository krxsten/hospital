using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.DTOs
{
    public class MedicationCreateDTO
    {
        public string Name { get; set; } = null!;
        public Guid DiagnoseID { get; set; }
        public string Description { get; set; } = null!;
        public string SideEffects { get; set; } = null!;
        public string DiagnoseName { get; set; } =null!;
    }
}
