using Hospital.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.DTOs
{
    public class DiagnoseIndexDTO
    {
        public Guid ID { get; set; }
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public List<Guid> PatientIDs { get; set; } = new();
        public List<Guid> MedicationIDs { get; set; } = new();
    }
}