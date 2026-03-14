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
    public class MedicationService : IMedicationService
    {
        private readonly HospitalDbContext context;

        public MedicationService(HospitalDbContext context)
        {
            this.context = context;
        }
        async Task IMedicationService.CreateAsync(MedicationCreateDTO model)
        {
            throw new NotImplementedException();
        }

        async Task IMedicationService.DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        async Task<List<MedicationIndexDTO>> IMedicationService.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        async Task<MedicationIndexDTO?> IMedicationService.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        async Task IMedicationService.UpdateAsync(MedicationIndexDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
