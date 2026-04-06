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
					UserID = x.UserId,
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
					UserID = x.UserId,
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
            var uploadResult = await imageService.UploadImageAsync(model.ImageFile);
            var nurse = new Nurse
			{
				ID = Guid.NewGuid(),
				UserId = model.UserID,
				SpecializationId = model.SpecializationId,
				ShiftId = model.ShiftId,
				IsAccepted = model.IsAccepted,
                ImageURL = uploadResult.Url,
                PublicID = uploadResult.PublicId
            };

			await context.Nurses.AddAsync(nurse);
			await context.SaveChangesAsync();
		}

		public async Task UpdateAsync(NurseIndexDTO model)
		{
			var nurse = await context.Nurses.FindAsync(model.ID);
			if (nurse == null)
			{
				return;
			}
            if (model.ImageURL != null)
            {
                var uploadResult = await imageService.UploadImageAsync(model.NewImageFile);
                nurse.ImageURL = uploadResult.Url;
            }
            nurse.SpecializationId = model.SpecializationId;
			nurse.ShiftId = model.ShiftId;
			nurse.IsAccepted = model.IsAccepted;

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
        public Task<List<NurseIndexDTO>> FilterBySpecialization(string specialization)
        {
            return context.Nurses.Where(d => d.Specialization.SpecializationName == specialization)
               .Select(x => new NurseIndexDTO
               {
                   ID = x.ID,
                   UserID = x.UserId,
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
                    UserID = x.UserId,
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
