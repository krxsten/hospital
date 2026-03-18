using Hospital.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Contracts
{
    public interface ISpecializationService
    {
        Task<List<SpecializationIndexDTO>> GetAllAsync();

        Task<SpecializationIndexDTO?> GetByIdAsync(Guid id);

        Task CreateAsync(SpecializationCreateDTO model);

        Task UpdateAsync(SpecializationIndexDTO model);

        Task DeleteAsync(Guid id);
    }
}
