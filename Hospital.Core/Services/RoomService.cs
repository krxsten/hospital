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
    public class RoomService : IRoomService
    {
        private readonly HospitalDbContext context;

        public RoomService(HospitalDbContext context)
        {
            this.context = context;
        }
		public async Task<List<RoomIndexDTO>> GetAllAsync()
		{
			return await context.Rooms
				.Select(r => new RoomIndexDTO
				{
					ID = r.ID,
					RoomNumber = r.RoomNumber,
					IsTaken = r.IsTaken
				})
				.OrderBy(x=>x.RoomNumber).ToListAsync();
		}

		public async Task<RoomIndexDTO?> GetByIdAsync(Guid id)
		{
			return await context.Rooms
				.Where(r => r.ID == id)
				.Select(r => new RoomIndexDTO
				{
					ID = r.ID,
					RoomNumber = r.RoomNumber,
					IsTaken = r.IsTaken
				})
				.FirstOrDefaultAsync();
		}

		public async Task CreateAsync(RoomCreateDTO model)
		{
			var room = new Room
			{
				ID = Guid.NewGuid(),
				RoomNumber = model.RoomNumber,
				IsTaken = model.IsTaken
			};

			await context.Rooms.AddAsync(room);
			await context.SaveChangesAsync();
		}

		public async Task UpdateAsync(RoomIndexDTO model)
		{
			var room = await context.Rooms.FindAsync(model.ID);
			if (room == null) return;

			room.RoomNumber = model.RoomNumber;
			room.IsTaken = model.IsTaken;

			await context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Guid id)
		{
			var room = await context.Rooms.FindAsync(id);
			if (room == null) return;

			context.Rooms.Remove(room);
			await context.SaveChangesAsync();
		}
		public async Task<List<RoomIndexDTO>> GetRoomsAfterNum(int? roomNum = null)
		{
            return await context.Rooms
				.Where(x=>x.RoomNumber > roomNum)
                .Select(r => new RoomIndexDTO
                {
                    ID = r.ID,
                    RoomNumber = r.RoomNumber,
                    IsTaken = r.IsTaken
                })
                .OrderBy(x => x.RoomNumber).ToListAsync();
        }
	}
}
