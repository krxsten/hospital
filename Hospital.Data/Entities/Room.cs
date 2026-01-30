using Hospital.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Entities
{
    public class Room
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public int RoomNumber { get; set; }
        [Required]
        public bool IsTaken { get; set; }
        public List<Patient> ListOfPatients { get; set; } = new List<Patient>();
        public int PatientsCount { get; set; }
    }
}