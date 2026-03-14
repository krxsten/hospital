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
    public class SpecializationService : ISpecializationService
    {
        private readonly HospitalDbContext context;

        public SpecializationService(HospitalDbContext context)
        {
            this.context = context;
        }
        async Task ISpecializationService.CreateAsync(SpecializationCreateDTO model)
        {
            throw new NotImplementedException();
        }

        async Task ISpecializationService.DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        async Task<List<SpecializationIndexDTO>> ISpecializationService.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        async Task<SpecializationIndexDTO?> ISpecializationService.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        async Task ISpecializationService.UpdateAsync(SpecializationIndexDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
