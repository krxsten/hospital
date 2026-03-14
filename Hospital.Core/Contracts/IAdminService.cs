using Hospital.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Contracts
{
    public interface IAdminService
    {
        Task<List<UserDTO>> GetAllAsync();

        Task<UserDTO?> GetByIdAsync(Guid id);

        Task CreateAsync(UserDTO model);

        Task UpdateAsync(UserDTO model);

        Task DeleteAsync(Guid id);
    }
}
