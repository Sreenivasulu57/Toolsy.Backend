using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<AdminUserDto>> GetAllUsersForAdminAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> GetByIdWithRolesAsync(int id);
    }
}
