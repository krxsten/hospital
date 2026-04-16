using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Services
{
    public class NurseService : INurseService
    {
		private readonly HospitalDbContext context;
        private readonly IImageService imageService;

        public NurseService(HospitalDbContext context, IImageService imageService)
		{
			this.context = context;
			this.imageService = imageService;
		}

		public async Task<List<NurseIndexDTO>> GetAllAsync()
		{
			return await context.Nurses
				.Select(x => new NurseIndexDTO
				{
					ID = x.ID,
					UserId = x.UserId,
					UserName = x.User.FirstName + " " + x.User.LastName,
					SpecializationId = x.SpecializationId,
					SpecializationName = x.Specialization.SpecializationName,
					ShiftId = x.ShiftId,
					ShiftName = x.Shift.Type,
					IsAccepted = x.IsAccepted,
					ImageURL = x.ImageURL
				})
				.ToListAsync();
		}

		public async Task<NurseIndexDTO?> GetByIdAsync(Guid id)
		{
			return await context.Nurses
				.Where(x => x.ID == id)
				.Select(x => new NurseIndexDTO
				{
					ID = x.ID,
					UserId = x.UserId,
					UserName = x.User.FirstName + " " + x.User.LastName,
					SpecializationId = x.SpecializationId,
					SpecializationName = x.Specialization.SpecializationName,
					ShiftId = x.ShiftId,
					ShiftName = x.Shift.Type,
					IsAccepted = x.IsAccepted,
					ImageURL = x.ImageURL
				})
				.FirstOrDefaultAsync();
		}

		public async Task CreateAsync(NurseCreateDTO model)
		{
            var names = model.NurseName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

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

            if (model.ImageFile == null)
            {	
                throw new Exception("Image is required");
            }

            var uploadResult = await imageService.UploadImageAsync(model.ImageFile);
            var nurse = new Nurse
			{
				ID = Guid.NewGuid(),
				UserId = user.Id,
				SpecializationId = model.SpecializationID,
				ShiftId = model.ShiftID,
				IsAccepted = true,
                ImageURL = uploadResult.Url,
                PublicID = uploadResult.PublicId
            };

			await context.Nurses.AddAsync(nurse);
			await context.SaveChangesAsync();
		}

		public async Task UpdateAsync(NurseEditDTO model)
		{
            var nurse = await context.Nurses
               .Include(d => d.User)
               .FirstOrDefaultAsync(d => d.ID == model.ID);

            if (nurse == null)
            {
                return;
            }

            var names = model.NurseName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            nurse.User.FirstName = names.Length > 0 ? names[0] : "";
            nurse.User.LastName = names.Length > 1 ? names[1] : "";

            if (model.NewImageFile != null)
            {
                var uploadResult = await imageService.UploadImageAsync(model.NewImageFile);

                nurse.ImageURL = uploadResult.Url;
                nurse.PublicID = uploadResult.PublicId;
            }

            nurse.SpecializationId = model.SpecializationId;
			nurse.ShiftId = model.ShiftId;
			nurse.IsAccepted = true;

			await context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Guid id)
		{
			var nurse = await context.Nurses.FindAsync(id);
			if (nurse == null)
			{
				return;
			}

			context.Nurses.Remove(nurse);
			await context.SaveChangesAsync();
		}
        public async Task<List<NurseIndexDTO>> FilterBySpecialization(string specialization)
        {
            if (string.IsNullOrWhiteSpace(specialization))
            {
                return new List<NurseIndexDTO>();
            }
            string pattern = $"%{specialization}%";
            return await context.Nurses.Include(x => x.Specialization).Where(x => EF.Functions.Like(x.Specialization.SpecializationName, pattern))
               .Select(x => new NurseIndexDTO
               {
                   ID = x.ID,
                   UserId = x.UserId,
                   UserName = x.User.FirstName + " " + x.User.LastName,
                   SpecializationId = x.SpecializationId,
                   SpecializationName = x.Specialization.SpecializationName,
                   ShiftId = x.ShiftId,
                   ShiftName = x.Shift.Type,
                   IsAccepted = x.IsAccepted,
                   ImageURL = x.ImageURL
               })
                .ToListAsync();
        }
        public Task<List<NurseIndexDTO>> SortByFirstName()
        {
            return context.Nurses.OrderBy(x => x.User.FirstName)
                .Select(x => new NurseIndexDTO
                {
                    ID = x.ID,
                    UserId = x.UserId,
                    UserName = x.User.FirstName + " " + x.User.LastName,
                    SpecializationId = x.SpecializationId,
                    SpecializationName = x.Specialization.SpecializationName,
                    ShiftId = x.ShiftId,
                    ShiftName = x.Shift.Type,
                    IsAccepted = x.IsAccepted,
                    ImageURL = x.ImageURL
                })
                .ToListAsync();
        }
    }
}
