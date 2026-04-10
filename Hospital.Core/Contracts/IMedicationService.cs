using Hospital.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Contracts
{
    public interface IMedicationService
    {
        Task<List<MedicationIndexDTO>> GetAllAsync();

        Task<MedicationIndexDTO?> GetByIdAsync(Guid id);

        Task CreateAsync(MedicationCreateDTO model);

        Task UpdateAsync(MedicationEditDTO model);

        Task DeleteAsync(Guid id);
        Task<List<MedicationIndexDTO>> GetMedicationsForSideEffect(string sideEffect);
    }
}
