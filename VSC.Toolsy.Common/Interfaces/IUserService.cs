using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Common.Interfaces
{
    public interface IUserService
    {
        Task<bool> ApproveUserAccountAsync(int id);
        Task<bool> DeleteUserAccountAsync(int id);
        Task<List<AdminUserDto>> GetAllUsersForAdminAsync();
        Task<AdminUserDto> GetUserById(int id);
        public Task<User> SaveAsync(RegisterUserDto registerUserDto);
    }
}
