using Hospital.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Entities
{
    public class Specialization
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string SpecializationName { get; set; }
        [Required]
        public string Image { get; set; }

        public List<Doctor> ListOfDoctors { get; set; } = new List<Doctor>();
        public List<Nurse> ListOfNurses { get; set; } = new List<Nurse>();
    }
}
