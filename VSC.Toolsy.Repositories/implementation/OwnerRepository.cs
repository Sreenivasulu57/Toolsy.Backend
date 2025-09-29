using VSC.Toolsy.Repositories.Data;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Common.Exceptions;
using VSC.Toolsy.Repositories.implementation;
using VSC.Toolsy.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace VSC.Toolsy.Repositories.Implementation
{
    public class OwnerRepository : Repository<Owner>, IOwnerRepository
    {
        public OwnerRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<Owner> GetByIdWithProfileAsync(Guid ownerId)
            => await Query()
                .Include(o => o.Profile)
                .FirstOrDefaultAsync(o => o.Id.Equals(ownerId)) ?? throw new OwnerNotFoundException($"Owner this Id :{ownerId} not found");


        public async Task<Owner> GetByOwnerId(Guid ownerId)

            => await Query()
            .FirstOrDefaultAsync(o => o.Id.Equals(ownerId)) ?? throw new OwnerNotFoundException($"Owner this Id :{ownerId} not found");

        public async Task<Owner> GetByProfileId(Guid profileId)
            => await Query()
                 .FirstOrDefaultAsync(ow => ow.ProfileId.Equals(profileId)) ?? throw new OwnerNotFoundException($"Owner With This ProfileId {profileId} Is Not Found");

    }
}
