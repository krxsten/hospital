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
    public class SpecializationService : ISpecializationService
    {
        private readonly HospitalDbContext context;
        private readonly IImageService imageService;


        public SpecializationService(HospitalDbContext context, IImageService imageService)
        {
            this.context = context;
			this.imageService = imageService;
        }
		public async Task<List<SpecializationIndexDTO>> GetAllAsync()
		{
			return await context.Specializations
				.Select(x => new SpecializationIndexDTO
				{
					ID = x.ID,
					SpecializationName = x.SpecializationName,
					ImageURL = x.ImageURL
				})
				.ToListAsync();
		}

		public async Task<SpecializationIndexDTO?> GetByIdAsync(Guid id)
		{
			return await context.Specializations
				.Where(x => x.ID == id)
				.Select(x => new SpecializationIndexDTO
				{
					ID = x.ID,
					SpecializationName = x.SpecializationName,
					ImageURL = x.ImageURL
				})
				.FirstOrDefaultAsync();
		}

        public async Task CreateAsync(SpecializationCreateDTO model)
        {
            var uploadResult = await imageService.UploadImageAsync(model.ImageFile);

            var specialization = new Specialization
            {
                ID = Guid.NewGuid(),
                SpecializationName = model.SpecializationName,
                ImageURL = uploadResult.Url,
                PublicID = uploadResult.PublicId
            };

            await context.Specializations.AddAsync(specialization);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SpecializationIndexDTO model)
        {
            var specialization = await context.Specializations.FindAsync(model.ID);

            if (specialization == null)
            {
                return;
            }

            if (model.NewImageFile != null)
            {
                if (!string.IsNullOrEmpty(specialization.PublicID))
                {
                    await imageService.DestroyImageAsync(specialization.PublicID);
                }

                var uploadResult = await imageService.UploadImageAsync(model.NewImageFile);

                specialization.ImageURL = uploadResult.Url;
                specialization.PublicID = uploadResult.PublicId;
            }

            specialization.SpecializationName = model.SpecializationName;

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
		{
			var specialization = await context.Specializations.FindAsync(id);
			if (specialization == null)
			{
				return;
			}

			context.Specializations.Remove(specialization);
			await context.SaveChangesAsync();
		}
		public async Task<List<SpecializationIndexDTO>> GetSpecialization(string specialization)
        {
            return await context.Specializations.Where(x => x.SpecializationName==specialization).Select(x => new SpecializationIndexDTO
            {
                ID = x.ID,
                SpecializationName = x.SpecializationName,
                ImageURL = x.ImageURL
            }).ToListAsync();
        }
    }
}
