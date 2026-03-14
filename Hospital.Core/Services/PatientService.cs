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
    public class PatientService : IPatientService
    {
        private readonly HospitalDbContext context;

        public PatientService(HospitalDbContext context)
        {
            this.context = context;
        }
        async Task IPatientService.CreateAsync(PatientCreateDTO model)
        {
            throw new NotImplementedException();
        }

        async Task IPatientService.DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        async Task<List<PatientIndexDTO>> IPatientService.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        async Task<PatientIndexDTO?> IPatientService.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        async Task IPatientService.UpdateAsync(PatientIndexDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
