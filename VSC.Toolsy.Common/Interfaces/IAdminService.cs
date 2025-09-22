using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSC.Toolsy.Common.DTOs.Responses;

namespace VSC.Toolsy.Common.Interfaces
{
    public interface IAdminService
    {
        Task<bool> ApproveProfileAccountAsync(string email);
        Task<bool> DeleteUserAccountAsync(string email);
        Task<List<AdminUserDto>> GetAllUsersForAdminAsync();
        Task<AdminUserDto> GetUserByEmailAsync(string email);
    }
}
