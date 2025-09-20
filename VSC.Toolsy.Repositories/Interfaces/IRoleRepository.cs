using VSC.Toolsy.Common.Models.BaseEntites;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Repositories.Interfaces
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role> GetByIdAsync(int id);
    }
}
