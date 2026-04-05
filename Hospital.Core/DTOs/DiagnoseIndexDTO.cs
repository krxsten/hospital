using Hospital.Entities;
using Microsoft.AspNetCore.Http;
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
        public string ImageURL { get; set; } = null!; 
        public IFormFile? NewImageFile { get; set; }

    }
}