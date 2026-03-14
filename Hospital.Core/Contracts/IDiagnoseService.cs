using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Data.Entities;
using Hospital.Core.DTOs;

namespace Hospital.Core.Contracts
{
    public interface IDiagnoseService
    {
        Task<List<DiagnoseIndexDTO>> GetAllAsync();

        Task<DiagnoseIndexDTO?> GetByIdAsync(Guid id);

        Task CreateAsync(DiagnoseCreateDTO model);

        Task UpdateAsync(DiagnoseIndexDTO model);

        Task DeleteAsync(Guid id);

    }
}
