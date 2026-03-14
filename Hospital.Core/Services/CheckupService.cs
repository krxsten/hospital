using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
using Hospital.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Services
{
    public class CheckupService : ICheckupService
    {
        private readonly HospitalDbContext context;

        public CheckupService(HospitalDbContext context)
        {
            this.context = context;
        }
        async Task ICheckupService.CreateAsync(CheckupCreateDTO model)
        {
            
        }

        async Task ICheckupService.DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        async Task<List<CheckupIndexDTO>> ICheckupService.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        async Task<CheckupIndexDTO?> ICheckupService.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        async Task ICheckupService.UpdateAsync(CheckupIndexDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
