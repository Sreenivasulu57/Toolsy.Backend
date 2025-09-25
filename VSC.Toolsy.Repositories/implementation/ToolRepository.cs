using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.Data;
using VSC.Toolsy.Repositories.Interfaces;

namespace VSC.Toolsy.Repositories.implementation
{
    public class ToolRepository : Repository<Tool>, IToolRepository
    {
        public ToolRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }
    }
}
