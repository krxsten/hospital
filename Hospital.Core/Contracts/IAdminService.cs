using Hospital.Core.DTOs;
using Hospital.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Contracts
{
    public interface IAdminService
    {
        Task<IEnumerable<Doctor>> GetPendingDoctorsAsync();
        Task<IEnumerable<Nurse>> GetPendingNursesAsync();

        Task<Doctor?> AcceptDoctorAsync(Guid id);
        Task<Doctor?> RejectDoctorAsync(Guid id);

        Task<Nurse?> AcceptNurseAsync(Guid id);
        Task<Nurse?> RejectNurseAsync(Guid id);
    }
}
