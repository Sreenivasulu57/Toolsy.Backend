using VSC.Toolsy.Repositories.Data;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.implementation;
using VSC.Toolsy.Repositories.Interfaces;

namespace VSC.Toolsy.Repositories.Implementation
{
    public class OwnerRepository : Repository<Owner>, IOwnerRepository
    {
        public OwnerRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
