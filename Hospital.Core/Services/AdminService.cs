using Hospital.Core.Contracts;
using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly HospitalDbContext context;
        private readonly UserManager<User> userManager;

        public AdminService(HospitalDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public async Task<Doctor?> AcceptDoctorAsync(Guid id)
        {
            var doctor = await context.Doctors.FirstOrDefaultAsync(d => d.ID == id);
            if (doctor == null) return null;

            doctor.IsAccepted = true;
            await context.SaveChangesAsync();
            return doctor;
        }

        public async Task<Nurse?> AcceptNurseAsync(Guid id)
        {

            var nurse = await context.Nurses.FirstOrDefaultAsync(d => d.ID == id);
            if (nurse == null) return null;

            nurse.IsAccepted = true;
            await context.SaveChangesAsync();
            return nurse;
        }

        public async Task<IEnumerable<Doctor>> GetPendingDoctorsAsync()
        {
            return await context.Doctors
        .Include(d => d.User)
        .Include(d => d.Specialization)
        .Include(d => d.Shift)
        .Where(d => !d.IsAccepted)
        .ToListAsync();
        }

        public async Task<IEnumerable<Nurse>> GetPendingNursesAsync()
        {
           return await context.Nurses
            .Include(n => n.User)
            .Include(d => d.Specialization)
            .Include(d => d.Shift)
            .Where(n => !n.IsAccepted)
            .ToListAsync();
        }

        public async Task<Doctor?> RejectDoctorAsync(Guid id)
        {
            var doctor = await context.Doctors.FindAsync(id);
            if (doctor == null) return null;

            var user = await userManager.FindByIdAsync(doctor.UserId.ToString());
            context.Doctors.Remove(doctor);
            await context.SaveChangesAsync();

            if (user != null) await userManager.DeleteAsync(user);
            return doctor;
        }

        public async Task<Nurse?> RejectNurseAsync(Guid id)
        {
            var nurse = await context.Nurses.FindAsync(id);
            if (nurse == null) return null;

            var user = await userManager.FindByIdAsync(nurse.UserId.ToString());
            context.Nurses.Remove(nurse);
            await context.SaveChangesAsync();

            if (user != null) await userManager.DeleteAsync(user);
            return nurse;
        }
    }
}
