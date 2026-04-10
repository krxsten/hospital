using Hospital.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Contracts
{
    public interface INurseService
    {
        Task<List<NurseIndexDTO>> GetAllAsync();

        Task<NurseIndexDTO?> GetByIdAsync(Guid id);

        Task CreateAsync(NurseCreateDTO model);

        Task UpdateAsync(NurseEditDTO model);

        Task DeleteAsync(Guid id);
        Task<List<NurseIndexDTO>> FilterBySpecialization(string specialization);
        Task<List<NurseIndexDTO>> SortByFirstName();
    }
}
