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

        Task UpdateAsync(CheckupIndexDTO model);

        Task DeleteAsync(Guid id);
    }
}
