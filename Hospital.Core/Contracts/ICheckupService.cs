using Hospital.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Contracts
{
    public interface ICheckupService
    {
        Task<List<CheckupIndexDTO>> GetAllAsync();

        Task<CheckupIndexDTO?> GetByIdAsync(Guid id);

        Task CreateAsync(CheckupCreateDTO model);

        Task UpdateAsync(CheckupEditDTO model);

        Task DeleteAsync(Guid id);
        Task<List<CheckupIndexDTO>> GetCheckupsDate(DateOnly date);
    }
}
