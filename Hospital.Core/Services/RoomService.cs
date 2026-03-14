using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
using Hospital.Data;
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
        async Task IRoomService.CreateAsync(RoomCreateDTO model)
        {
            throw new NotImplementedException();
        }

        async Task IRoomService.DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        async Task<List<RoomIndexDTO>> IRoomService.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        async Task<RoomIndexDTO?> IRoomService.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        async Task IRoomService.UpdateAsync(RoomIndexDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
