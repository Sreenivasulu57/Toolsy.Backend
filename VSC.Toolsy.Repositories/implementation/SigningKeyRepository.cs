using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.Data;
using VSC.Toolsy.Repositories.Interfaces;

namespace VSC.Toolsy.Repositories.implementation
{
    public class SigningKeyRepository : Repository<SigningKey>, ISigningKeyRepository
    {
        public SigningKeyRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

    }
}
