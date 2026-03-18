using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
using Hospital.Data;
using Hospital.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly HospitalDbContext context;

        public DoctorService(HospitalDbContext context)
        {
            this.context = context;
        }
        async Task IDoctorService.CreateAsync(DoctorCreateDto dto)
        {

            var doctor = new Doctor
            {
                UserId = dto.UserID,
                SpecializationId = dto.SpecializationID,
                ShiftId = dto.ShiftID,
                IsAccepted = dto.IsAccepted,
                Image = dto.Image
            };

            await context.Doctors.AddAsync(doctor);
            await context.SaveChangesAsync();
        }

        async Task IDoctorService.DeleteAsync(Guid id)
        {
            var doctor = await context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return;
            }

            context.Doctors.Remove(doctor);
            await context.SaveChangesAsync();
        }

        async Task<List<DoctorIndexDto>> IDoctorService.GetAllAsync()
        {
            return await context.Doctors
               .Select(d => new DoctorIndexDto
               {
                   UserId = d.UserId,
                   UserName = d.User.FirstName + d.User.LastName,
                   SpecializationId = d.SpecializationId,
                   SpecializationName = d.Specialization.SpecializationName,
                   ShiftId = d.ShiftId,
                   ShiftName = d.Shift.Type,
                   IsAccepted = d.IsAccepted,
                   Image = d.Image
               })
               .ToListAsync();
        }

		async Task<DoctorIndexDto?> IDoctorService.GetByIdAsync(Guid id)
		{
			return await context.Doctors
				.Where(d => d.ID == id)
				.Select(d => new DoctorIndexDto
				{
					ID = d.ID,
					UserId = d.UserId,
					UserName = d.User.FirstName + " " + d.User.LastName,
					SpecializationId = d.SpecializationId,
					SpecializationName = d.Specialization.SpecializationName,
					ShiftId = d.ShiftId,
					ShiftName = d.Shift.Type,
					IsAccepted = d.IsAccepted,
					Image = d.Image
				})
				.FirstOrDefaultAsync();
		}

		async Task IDoctorService.UpdateAsync(DoctorIndexDto dto)
		{
			var doctor = await context.Doctors.FindAsync(dto.ID);
			if (doctor == null)
			{
				return;
			}

			doctor.SpecializationId = dto.SpecializationId;
			doctor.ShiftId = dto.ShiftId;
			doctor.IsAccepted = dto.IsAccepted;
			doctor.Image = dto.Image;

			await context.SaveChangesAsync();
		}
	}
}
