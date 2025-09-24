using VSC.Toolsy.Repositories.Data;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Common.Exceptions;
using VSC.Toolsy.Repositories.implementation;
using VSC.Toolsy.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace VSC.Toolsy.Repositories.Implementation
{
    public class OwnerRepository : Repository<Owner>, IOwnerRepository
    {
        public OwnerRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<Owner> GetByProfileId(Guid profileId)
            => await Query()
                 .FirstOrDefaultAsync(ow => ow.ProfileId.Equals(profileId))?? throw new OwnerNotFoundException($"Owner With This ProfileId {profileId} Is Not Found");
        
    }
}
