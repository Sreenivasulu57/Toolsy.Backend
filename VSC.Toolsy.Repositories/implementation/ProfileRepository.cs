using Microsoft.EntityFrameworkCore;
using VSC.Toolsy.Common.Enums;
using VSC.Toolsy.Common.Exceptions;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.Data;
using VSC.Toolsy.Repositories.Interfaces;

namespace VSC.Toolsy.Repositories.implementation
{
    public class ProfileRepository : IProfileRepository
    {

        public int Save(Profile profile)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Profiles.Add(profile);
                return context.SaveChanges();
            }
        }

        public async Task<int> SaveAsync(Profile profile)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                await context.Profiles.AddAsync(profile);
                return await context.SaveChangesAsync();
            }
        }


        public List<Profile> GetAll()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Profiles.ToList();
            }
        }

        public async Task<List<Profile>> GetAllAsync()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Profiles.ToListAsync();
            }
        }

        public async Task<List<Profile>> GetAllUserAsync()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Profiles
                     .Where(p => p.Roles.Contains(UserRole.User))
                     .ToListAsync();
            }
        }

        public async Task<Profile> GetByProfileId(Guid profileId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Profiles
                     .FirstOrDefaultAsync(p => p.Id.Equals(profileId))
                     ?? throw new UserNotFoundException($"User With This profileId : {profileId} Is Not Found");
            }
        }

        public async Task<Profile> GetByEmailAsync(string email)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {

                return await context.Profiles
                     .FirstOrDefaultAsync(p => p.Email.Equals(email))
                     ?? throw new UserNotFoundException($"User With This email : {email} Is Not Found");
            }
        }
        public async Task<Profile> GetProfileByPhoneNumberAsync(string phoneNumber)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Profiles
                    .FirstOrDefaultAsync(p => p.PhoneNumber.Equals(phoneNumber))
                    ?? throw new UserNotFoundException($"User With This phonenumber : {phoneNumber} Is Not Found");
            }
        }
        public async Task<Profile> GetProfileWithAddressByEmailAsync(string email)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Profiles
                   .Include(p => p.Address)
                   .FirstOrDefaultAsync(p => p.Email.Equals(email))
                   ?? throw new UserNotFoundException($"User With This Email {email} Is Not Found");
            }
        }

        public async Task<Profile> GetProfileWithAddressByProfileId(Guid profileId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Profiles
                   .Include(p => p.Address)
                   .FirstOrDefaultAsync(p => p.Id.Equals(profileId))
                   ?? throw new UserNotFoundException($"User With This ProfileId {profileId} Is Not Found");
            }
        }


        public async Task<int> UpdateAsync(Profile profile)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Profiles.Update(profile);
                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteAsync(Profile profile)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Profiles.Remove(profile);
                return await context.SaveChangesAsync();
            }
        }
    }
}
