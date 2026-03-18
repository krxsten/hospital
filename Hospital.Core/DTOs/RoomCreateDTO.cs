using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.DTOs
{
    public class RoomCreateDTO
    {
        public int RoomNumber { get; set; }
        public bool IsTaken { get; set; }
       
    }
}
