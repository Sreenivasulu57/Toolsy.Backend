using Microsoft.EntityFrameworkCore;
using VSC.Toolsy.Common.Enums;
using VSC.Toolsy.Common.Exceptions;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.Data;
using VSC.Toolsy.Repositories.Interfaces;

namespace VSC.Toolsy.Repositories.implementation
{
    public class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        public ProfileRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

        public async Task<List<Profile>> GetAllUserAsync()
            => await Query()
                .Where(p => p.Role.Equals(Role.User))
                .ToListAsync();

        public async Task<Profile> GetByEmailAsync(string email)
            => await Query()
            .FirstOrDefaultAsync(p => p.Email.Equals(email)) ?? throw new UserNotFoundException($"User With This email : {email} Is Not Found");


        public async Task<Profile> GetByProfileId(Guid profileId)
            => await Query()
            .FirstOrDefaultAsync(p => p.Id.Equals(profileId)) ?? throw new UserNotFoundException($"User With This profileId : {profileId} Is Not Found");

        public async Task<Profile> GetProfileByPhoneNumberAsync(string phoneNumber)
            => await Query()
            .FirstOrDefaultAsync(p => p.PhoneNumber.Equals(phoneNumber)) ?? throw new UserNotFoundException($"User With This phonenumber : {phoneNumber} Is Not Found");

        public async Task<Profile> GetProfileWithAddressByEmailAsync(string email)
            => await Query()
            .Include(p => p.Address)
            .FirstOrDefaultAsync(p => p.Email.Equals(email)) ?? throw new UserNotFoundException($"User With This Email {email} Is Not Found");

        public Task<Profile> GetProfileWithAddressByProfileId(Guid profileId)
        {
            throw new NotImplementedException();
        }
    }
}
