using Hospital.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Contracts
{
    public interface IDoctorService
    {
        Task<List<DoctorIndexDto>> GetAllAsync();

        Task<DoctorIndexDto?> GetByIdAsync(Guid id);

        Task CreateAsync(DoctorCreateDto model);

        Task UpdateAsync(DoctorIndexDto model);

        Task DeleteAsync(Guid id);
        Task<List<DoctorIndexDto>> FilterBySpecialization(string specialization);
    }
}
