using VSC.Toolsy.Common.DTOs.Responses;

namespace VSC.Toolsy.Common.Interfaces
{
    public interface IAdminService
    {
        Task<bool> ApproveProfileAccountAsync(Guid profileId);
        Task<bool> DeleteUserAccountAsync(Guid profileId);
        Task<List<AdminUserDto>> GetAllUsersForAdminAsync();
        Task<AdminUserDto> GetUserByProfileIdAsync(Guid profileId);
    }
}
