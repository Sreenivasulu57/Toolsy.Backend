using System.Data;
using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Enums;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.Interfaces;


namespace VSC.Toolsy.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;


        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> ApproveUserAccountAsync(int id)
        {
            User userFromDb = await _userRepository.GetByIdAsync(id);

            if (userFromDb.VerificationStatus == VerificationStatus.Verified)
            {
                return true; 
            }

            userFromDb.VerificationStatus = VerificationStatus.Verified;
            userFromDb.UpdatedAt = DateTime.UtcNow;
            userFromDb.UpdatedBy = "Admin";

            _userRepository.Update(userFromDb);

            int result = await _userRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteUserAccountAsync(int id)
        {
            DateTime now = DateTime.UtcNow;

            User userFromDb = await _userRepository.GetByIdWithRolesAsync(id);

            if (userFromDb.IsDeleted)
            {
                return true; 
            }

            userFromDb.IsDeleted = true;
            userFromDb.DeletedAt = now;
            userFromDb.DeletedBy = "Admin"; //Todo Take From Admin

            foreach (UserRole ur in userFromDb.UserRoles) {
                ur.IsActive = false;
                ur.UpdatedAt = now;
            }

            _userRepository.Update(userFromDb);

            int result = await _userRepository.SaveChangesAsync();

            return result > 0;
            
        }

        //For Admin view
        public async Task<List<AdminUserDto>>  GetAllUsersForAdminAsync()
            => await _userRepository.GetAllUsersForAdminAsync();

        public async Task<AdminUserDto> GetUserById(int id)
        {
            User userFromDb = await _userRepository.GetByIdWithRolesAsync(id);

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
                PhoneVerifiedAt = userFromDb.PhoneVerifiedAt,
                Roles = userFromDb.UserRoles.Select(ur => new RoleDto
                {
                    RoleName = ur.Role.Name,
                    IsActive = ur.IsActive
                }).ToList()
            };

        } 
        public async Task<User> SaveAsync(RegisterUserDto registerUserDto)
        {
            User user = new
       User
            {

                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                Email = registerUserDto.Email,
                PhoneNumber = registerUserDto.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password),
                ProfileImageUrl = registerUserDto.ProfileImageUrl,
                CreatedBy = registerUserDto.Email,
            };

            await _userRepository.SaveAsync(user);

            await _userRepository.SaveChangesAsync();

            return user;
        }
    }
}