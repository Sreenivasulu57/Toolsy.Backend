using Microsoft.EntityFrameworkCore;
using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Enums;
using VSC.Toolsy.Common.Exceptions;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.Data;
using VSC.Toolsy.Repositories.Interfaces;

namespace VSC.Toolsy.Repositories.implementation
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

        public async Task<List<AdminUserDto>> GetAllUsersForAdminAsync()
        {
            return await Query()
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
                    PhoneVerifiedAt = u.PhoneVerifiedAt,
                    Roles = u.UserRoles.Select(ur => new RoleDto
                    {
                        RoleName = ur.Role.Name,
                        IsActive = ur.IsActive
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
            => await Query()
            .FirstOrDefaultAsync(u => u.Id.Equals(id)) ?? throw new UserNotFoundException($"User With This Id {id} Is Not Found");

        public async Task<User> GetByIdWithRolesAsync(int id)
        {
            return await Query()
                 .Include(u => u.UserRoles)
                 .ThenInclude(ur => ur.Role)
                 .FirstOrDefaultAsync(u => u.Id.Equals(id)) ?? throw new UserNotFoundException($"User With This Id {id} Is Not Found");


        }
    }
}
