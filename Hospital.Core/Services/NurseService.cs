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
    public class NurseService : INurseService
    {
        private readonly HospitalDbContext context;

        public NurseService(HospitalDbContext context)
        {
            this.context = context;
        }
        async Task INurseService.CreateAsync(NurseCreateDTO model)
        {
            throw new NotImplementedException();
        }

        async Task INurseService.DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        async Task<List<NurseIndexDTO>> INurseService.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        async Task<NurseIndexDTO?> INurseService.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        async Task INurseService.UpdateAsync(NurseCreateDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
