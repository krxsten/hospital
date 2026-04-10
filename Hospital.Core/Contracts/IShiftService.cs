using Hospital.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Contracts
{
    public interface IShiftService
    {
        Task<List<ShiftIndexDTO>> GetAllAsync();

        Task<ShiftIndexDTO?> GetByIdAsync(Guid id);

        Task CreateAsync(ShiftCreateDTO model);

        Task UpdateAsync(ShiftIndexDTO model);

        Task DeleteAsync(Guid id);
        Task<List<ShiftIndexDTO>> GetShiftByTime(TimeOnly time);
    }
}
