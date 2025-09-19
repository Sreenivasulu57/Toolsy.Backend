using VSC.Toolsy.Repositories.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Common.Exceptions;
using VSC.Toolsy.Repositories.Data;
using Microsoft.EntityFrameworkCore;

namespace VSC.Toolsy.Repositories.implementation
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

        public async Task<User> GetByIdAsync(int id)
            => await Query()
            .FirstOrDefaultAsync(u => u.Id.Equals(id)) ?? throw new UserNotFoundException($"User With This Id {id} Is Not Found");

    }
}
