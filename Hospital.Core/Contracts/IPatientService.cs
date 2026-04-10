using Hospital.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Contracts
{
    public interface IPatientService
    {
        Task<List<PatientIndexDTO>> GetAllAsync();

        Task<PatientIndexDTO?> GetByIdAsync(Guid id);

        Task CreateAsync(PatientCreateDTO model);

        Task UpdateAsync(PatientEditDTO model);

        Task DeleteAsync(Guid id);
        Task<PatientIndexDTO> Details(Guid id);
        Task SelectDoctorAndRoomAsync(Guid userId, Guid doctorId, Guid roomId,
            string birthCity, DateOnly dateOfBirth, string phoneNumber, string ucn);
        Task<List<PatientIndexDTO>> PatientsWithSuchDoctor(string doctroName);
    }
}
