using Microsoft.EntityFrameworkCore;
using VSC.Toolsy.Common.Exceptions;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.Data;
using VSC.Toolsy.Repositories.implementation;
using VSC.Toolsy.Repositories.Interfaces;

namespace VSC.Toolsy.Repositories.Implementation
{
    public class OwnerRepository : IOwnerRepository
    {
        public int Save(Owner owner)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Owners.Add(owner);
                return context.SaveChanges();
            }
        }

        public async Task<int> SaveAsync(Owner owner)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                await context.Owners.AddAsync(owner);
                return await context.SaveChangesAsync();
            }
        }


        public List<Owner> GetAll()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Owners.ToList();
            }
        }

        public async Task<List<Owner>> GetAllAsync()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Owners.ToListAsync();
            }
        }



        public async Task<int> UpdateAsync(Owner owner)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Owners.Update(owner);
                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteAsync(Owner owner)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Owners.Remove(owner);
                return await context.SaveChangesAsync();
            }
        }


        public async Task<Owner> GetByIdWithProfileAsync(Guid ownerId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Owners
                      .Include(o => o.Profile)
                      .FirstOrDefaultAsync(o => o.Id.Equals(ownerId)) ?? throw new OwnerNotFoundException($"Owner this Id :{ownerId} not found");
            }
        }

        public async Task<Owner> GetByOwnerId(Guid ownerId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Owners
                      .FirstOrDefaultAsync(o => o.Id.Equals(ownerId)) ?? throw new OwnerNotFoundException($"Owner this Id :{ownerId} not found");
            }
        }

        public async Task<Owner> GetByProfileId(Guid profileId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Owners
                    .FirstOrDefaultAsync(ow => ow.ProfileId.Equals(profileId)) ?? throw new OwnerNotFoundException($"Owner With This ProfileId {profileId} Is Not Found");
            }
        }

    }
}
