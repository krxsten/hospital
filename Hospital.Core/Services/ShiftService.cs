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
    public class ShiftService : IShiftService
    {
        private readonly HospitalDbContext context;

        public ShiftService(HospitalDbContext context)
        {
            this.context = context;
        }
        async Task IShiftService.CreateAsync(ShiftCreateDTO model)
        {
            throw new NotImplementedException();
        }

        async Task IShiftService.DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        async Task<List<ShiftIndexDTO>> IShiftService.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        async Task<ShiftIndexDTO?> IShiftService.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        async Task IShiftService.UpdateAsync(ShiftIndexDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
