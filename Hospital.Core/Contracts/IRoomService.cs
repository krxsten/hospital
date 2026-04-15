using Hospital.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Contracts
{
    public interface IRoomService
    {
        Task<List<RoomIndexDTO>> GetAllAsync();

        Task<RoomIndexDTO?> GetByIdAsync(Guid id);

        Task CreateAsync(RoomCreateDTO model);

        Task UpdateAsync(RoomIndexDTO model);

        Task DeleteAsync(Guid id);
        Task<List<RoomIndexDTO>> GetRoomsAfterNum(int? roomNum = null);
    }
}
