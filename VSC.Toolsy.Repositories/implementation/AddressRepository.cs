using Microsoft.EntityFrameworkCore;
using VSC.Toolsy.Common.Exceptions;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.Data;
using VSC.Toolsy.Repositories.Interfaces;

namespace VSC.Toolsy.Repositories.implementation
{
    public class AddressRepository : IAddressRepository
    {
        public int Save(Address address)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Addresses.Add(address);
                return context.SaveChanges();
            }
        }

        public async Task<int> SaveAsync(Address address)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                await context.Addresses.AddAsync(address);
                return await context.SaveChangesAsync();
            }
        }


        public List<Address> GetAll()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Addresses.Where(a => !a.ProfileId.Equals(Guid.Empty)).ToList();
            }
        }

        public async Task<List<Address>> GetAllAsync()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Addresses.Where(a => !a.ProfileId.Equals(Guid.Empty)).ToListAsync();
            }
        }



        public async Task<int> UpdateAsync(Address address)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Addresses.Update(address);
                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteAsync(Address address)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Addresses.Remove(address);
                return await context.SaveChangesAsync();
            }
        }

        public async Task<Address> GetByProfileId(Guid profileId)
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Addresses.Where(a => a.ProfileId.Equals(profileId))
                    .FirstOrDefaultAsync()
                    ?? throw new AddressNotFoundException($"Address is not found for this id {profileId}");
            }
        }
    }
}
