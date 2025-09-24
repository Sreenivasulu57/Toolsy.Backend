using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Enums;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUserService _userService;

        public AdminService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> ApproveProfileAccountAsync(string email)
        {

            Profile userFromDb = await _userService.GetByEmailAsync(email);

            if (userFromDb.VerificationStatus == VerificationStatus.Verified)
            {
                return true;
            }

            userFromDb.VerificationStatus = VerificationStatus.Verified;
            userFromDb.UpdatedAt = DateTime.UtcNow;
            userFromDb.UpdatedBy = Role.Admin.ToString();

            int result = await _userService.UpdateUserAsync(userFromDb);

            return result > 0;

        }

        public async Task<bool> DeleteUserAccountAsync(string email)
        {
            DateTime now = DateTime.UtcNow;

            Profile userFromDb = await _userService.GetByEmailAsync(email);

            if (userFromDb.IsDeleted)
            {
                return true;
            }

            userFromDb.IsActive = false;
            userFromDb.IsDeleted = true;
            userFromDb.DeletedAt = now;
            userFromDb.DeletedBy = Role.Admin.ToString();

            int result = await _userService.UpdateUserAsync(userFromDb);

            return result > 0;

        }

        public async Task<List<AdminUserDto>> GetAllUsersForAdminAsync()
            => (await _userService.GetAllUserAsync())
            .Select(u => new AdminUserDto
            {
                UserId = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber ?? string.Empty,
                ProfileImageUrl = u.ProfileImageUrl ?? string.Empty,
                IsDeleted = u.IsDeleted,
                Status = u.Status,
                VerificationStatus = u.VerificationStatus,
                EmailVerifiedAt = u.EmailVerifiedAt,
                PhoneVerifiedAt = u.PhoneVerifiedAt
            }).ToList();

        public async Task<AdminUserDto> GetUserByEmailAsync(string email)
        {

            Profile userFromDb = (await _userService.GetByEmailAsync(email));

            return new AdminUserDto
            {
                UserId = userFromDb.Id,
                FirstName = userFromDb.FirstName,
                LastName = userFromDb.LastName,
                Email = userFromDb.Email,
                PhoneNumber = userFromDb.PhoneNumber ?? string.Empty,
                ProfileImageUrl = userFromDb.ProfileImageUrl ?? string.Empty,
                IsDeleted = userFromDb.IsDeleted,
                Status = userFromDb.Status,
                VerificationStatus = userFromDb.VerificationStatus,
                EmailVerifiedAt = userFromDb.EmailVerifiedAt,
                PhoneVerifiedAt = userFromDb.PhoneVerifiedAt
            };

        }


    }
}
