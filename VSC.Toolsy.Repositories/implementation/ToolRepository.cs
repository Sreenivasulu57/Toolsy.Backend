using Microsoft.EntityFrameworkCore;
using VSC.Toolsy.Common.Exceptions;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.Data;
using VSC.Toolsy.Repositories.Interfaces;

namespace VSC.Toolsy.Repositories.implementation
{
    public class ToolRepository : IToolRepository
    {
        public int Save(Tool tool)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Tools.Add(tool);
                return context.SaveChanges();
            }
        }

        public async Task<int> SaveAsync(Tool tool)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                await context.Tools.AddAsync(tool);
                return await context.SaveChangesAsync();
            }
        }

        public List<Tool> GetAll()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Tools.Where(t => !t.IsDeleted).ToList();
            }
        }

        public async Task<List<Tool>> GetAllAsync()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Tools.Where(t => !t.IsDeleted).ToListAsync();
            }
        }

        public async Task<int> UpdateAsync(Tool tool)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Tools.Update(tool);
                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteAsync(Tool tool)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Tools.Remove(tool);
                return await context.SaveChangesAsync();
            }
        }

        public async Task<List<Tool>> GetAllByOwnerId(Guid ownerId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Tools
                    .Where(t => !t.IsDeleted)
                     .Where(t => t.OwnerId.Equals(ownerId))
                     .Include(t => t.ToolImages)
                     .ToListAsync() ?? throw new OwnerNotFoundException($"Owner not found for this id :{ownerId}");
            }
        }

        public async Task<List<Tool>> GetAllWithImagesAsync()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Tools
                    .Where(t => !t.IsDeleted)
                    .Include(t => t.ToolImages)
                    .ToListAsync();
            }
        }

        public async Task<Tool> GetByToolId(Guid toolId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Tools
                    .Where(t => !t.IsDeleted)
                    .Where(t => t.Id.Equals(toolId))
                    .FirstOrDefaultAsync() ?? throw new ToolNotFoundException($"Tool this Id {toolId} is not found");
            }
        }


    }
}
