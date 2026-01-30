using Hospital.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Data.Entities
{
    public class Shift
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public DateTime StartTime { get; set; } = new DateTime();
        [Required]
        public DateTime EndTime { get; set; } = new DateTime();
        public List<Doctor> ListOfDoctors { get; set; } = new List<Doctor>();
        public List<Nurse> ListOfNurses { get; set; } = new List<Nurse>();
    }
}
