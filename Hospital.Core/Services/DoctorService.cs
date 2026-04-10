using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly HospitalDbContext context;
        private readonly IImageService imageService;
        private readonly UserManager<User> userManager;

        public DoctorService(HospitalDbContext context, 
            IImageService _imageService,
            UserManager<User> userManager) 
        {
            this.context = context;
            this.imageService = _imageService;
            this.userManager = userManager;
        }
        async Task IDoctorService.CreateAsync(DoctorCreateDto dto)
        {
            var names = dto.DoctorName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var firstName = names.Length > 0 ? names[0] : "";
            var lastName = names.Length > 1 ? names[1] : "";

            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                UserName = firstName + "_" + lastName
            };

            await context.Users.AddAsync(user);

            if (dto.ImageFile == null)
            {
                throw new Exception("Image is required");
            }

            var uploadResult = await imageService.UploadImageAsync(dto.ImageFile);

            var doctor = new Doctor
            {
                UserId = user.Id,
                SpecializationId = dto.SpecializationID,
                ShiftId = dto.ShiftID,
                IsAccepted = dto.IsAccepted,
                ImageURL = uploadResult.Url,
                CloudinaryID = uploadResult.PublicId
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
                   ID = d.ID,
                   UserId = d.UserId,
                   UserName = d.User.FirstName + " " + d.User.LastName,
                   SpecializationId = d.SpecializationId,
                   SpecializationName = d.Specialization.SpecializationName,
                   ShiftId = d.ShiftId,
                   ShiftName = d.Shift.Type,
                   IsAccepted = d.IsAccepted,
                   ImageURL = d.ImageURL
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
                    ImageURL = d.ImageURL
                    
                })
				.FirstOrDefaultAsync();
		}

        async Task IDoctorService.UpdateAsync(DoctorEditDTO dto)
        {
            var doctor = await context.Doctors
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.ID == dto.ID);

            if (doctor == null)
            {
                return;
            }

            var names = dto.DoctorName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            doctor.User.FirstName = names.Length > 0 ? names[0] : "";
            doctor.User.LastName = names.Length > 1 ? names[1] : "";

            if (dto.NewImageFile != null)
            {
                var uploadResult = await imageService.UploadImageAsync(dto.NewImageFile);

                doctor.ImageURL = uploadResult.Url;
                doctor.CloudinaryID = uploadResult.PublicId;
            }
            doctor.SpecializationId = dto.SpecializationId;
            doctor.ShiftId = dto.ShiftId;
            doctor.IsAccepted = dto.IsAccepted;

            await context.SaveChangesAsync();
        }

        public async Task<List<DoctorIndexDto>> FilterBySpecialization(string specialization)
        {
            if (string.IsNullOrWhiteSpace(specialization))
            {
                return new List<DoctorIndexDto>();
            }
            string pattern = $"%{specialization}%";
            return await context.Doctors.Include(x=>x.Specialization).Where(x => EF.Functions.Like(x.Specialization.SpecializationName, pattern))
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
                    ImageURL = d.ImageURL
                }).ToListAsync();
        }
        public Task<List<DoctorIndexDto>> SortByFirstName()
        {
            return context.Doctors.OrderBy(x=>x.User.FirstName)
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
                    ImageURL = d.ImageURL
                }).ToListAsync();
        }

    }
}
