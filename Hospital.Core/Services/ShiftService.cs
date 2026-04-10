using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
using Hospital.Data;
using Hospital.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Services
{
    public class ShiftService : IShiftService
    {
        private readonly HospitalDbContext context;

        public ShiftService(HospitalDbContext context)
        {
            this.context = context;
        }
		public async Task<List<ShiftIndexDTO>> GetAllAsync()
		{
			return await context.Shifts
				.Select(s => new ShiftIndexDTO
				{
					ID = s.ID,
					Type = s.Type,
					StartTime = s.StartTime,
					EndTime = s.EndTime
				})
				.ToListAsync();
		}

		public async Task<ShiftIndexDTO?> GetByIdAsync(Guid id)
		{
			return await context.Shifts
				.Where(s => s.ID == id)
				.Select(s => new ShiftIndexDTO
				{
					ID = s.ID,
					Type = s.Type,
					StartTime = s.StartTime,
					EndTime = s.EndTime
				})
				.FirstOrDefaultAsync();
		}

		public async Task CreateAsync(ShiftCreateDTO model)
		{
			if (model.EndTime <= model.StartTime)
			{
				return; 
			}

			var shift = new Shift
			{
				ID = Guid.NewGuid(),
				Type = model.Type,
				StartTime = model.StartTime,
				EndTime = model.EndTime
			};

			await context.Shifts.AddAsync(shift);
			await context.SaveChangesAsync();
		}

		public async Task UpdateAsync(ShiftIndexDTO model)
		{
			if (model.EndTime <= model.StartTime)
			{
				return; 
			}

			var shift = await context.Shifts.FindAsync(model.ID);
			if (shift == null) return;

			shift.Type = model.Type;
			shift.StartTime = model.StartTime;
			shift.EndTime = model.EndTime;

			await context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Guid id)
		{
			var shift = await context.Shifts.FindAsync(id);
			if (shift == null) return;

			context.Shifts.Remove(shift);
			await context.SaveChangesAsync();
		}
        public async Task<List<ShiftIndexDTO>> GetShiftByTime(TimeOnly time)
        {
            return await context.Shifts
                .Where(s => (s.StartTime <= s.EndTime && time >= s.StartTime && time <= s.EndTime) || (s.StartTime > s.EndTime && (time >= s.StartTime || time <= s.EndTime)))
                .Select(s => new ShiftIndexDTO
                {
                    ID = s.ID,
                    Type = s.Type,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime
                })
                .ToListAsync();
        }
    }
}
