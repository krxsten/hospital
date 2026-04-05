using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.DTOs
{
    public class DiagnoseCreateDTO
    {
        public string Name { get; set; } = null!;
        public IFormFile ImageFile { get; set; } = null!;
       
    }
}
