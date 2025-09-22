using Microsoft.EntityFrameworkCore;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.Data;
using VSC.Toolsy.Repositories.Interfaces;
using VSC.Toolsy.Common.Exceptions;

namespace VSC.Toolsy.Repositories.implementation
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

        public async Task<Role> GetByIdAsync(int id) 
            => await Query()
                .FirstOrDefaultAsync(r => r.Id.Equals(id)) ?? throw new RoleNotFoundExcepiton($"The Role With This Id {id} Is Not Found");
        
    }
}
