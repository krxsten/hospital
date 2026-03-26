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

        public SpecializationService(HospitalDbContext context)
        {
            this.context = context;
        }
		public async Task<List<SpecializationIndexDTO>> GetAllAsync()
		{
			return await context.Specializations
				.Select(x => new SpecializationIndexDTO
				{
					ID = x.ID,
					SpecializationName = x.SpecializationName,
					Image = x.ImageURL
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
					Image = x.ImageURL
				})
				.FirstOrDefaultAsync();
		}

		public async Task CreateAsync(SpecializationCreateDTO model)
		{
			var specialization = new Specialization
			{
				ID = Guid.NewGuid(),
				SpecializationName = model.SpecializationName,
				ImageURL = model.File
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

			specialization.SpecializationName = model.SpecializationName;
			specialization.ImageURL = model.Image;

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
	}
}
