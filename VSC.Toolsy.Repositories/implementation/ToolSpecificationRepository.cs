using Microsoft.EntityFrameworkCore;
using VSC.Toolsy.Common.Exceptions;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.Data;
using VSC.Toolsy.Repositories.Interfaces;

namespace VSC.Toolsy.Repositories.implementation
{
    public class ToolSpecificationRepository : IToolSpecificationRepository
    {
        public int Delete(ToolSpecification toolSpecification)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.ToolSpecifications.Remove(toolSpecification);
                return context.SaveChanges();
            }
        }

        public async Task<int> DeleteAsync(ToolSpecification toolSpecification)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.ToolSpecifications.Remove(toolSpecification);
                return await context.SaveChangesAsync();
            }
        }

        public List<ToolSpecification> GetAll()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.ToolSpecifications.Where(ts => !ts.IsDeleted).ToList();
            }
        }

        public async Task<List<ToolSpecification>> GetAllAsync()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.ToolSpecifications.Where(ts => !ts.IsDeleted).ToListAsync();
            }
        }

        public ToolSpecification GetById(Guid toolSpecificationId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.ToolSpecifications.FirstOrDefault(ts => ts.Id.Equals(toolSpecificationId) && !ts.IsDeleted) ??
                    throw new ToolSpecificationNotFoundException("ToolSpecifications not found");
            }
        }

        public async Task<ToolSpecification> GetByIdAsync(Guid toolSpecificationId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.ToolSpecifications.FirstOrDefaultAsync(ts => ts.Id.Equals(toolSpecificationId) && !ts.IsDeleted)
                     ?? throw new ToolSpecificationNotFoundException($"ToolSpecification with this id {toolSpecificationId} not found ");
            }
        }

        public int Save(ToolSpecification toolSpecification)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.ToolSpecifications.Add(toolSpecification);
                return context.SaveChanges();
            }
        }

        public async Task<int> SaveAsync(ToolSpecification toolSpecification)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                await context.ToolSpecifications.AddAsync(toolSpecification);
                return await context.SaveChangesAsync();
            }
        }

        public int Update(ToolSpecification toolSpecification)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.ToolSpecifications.Update(toolSpecification);
                return context.SaveChanges();
            }
        }

        public async Task<int> UpdateAsync(ToolSpecification toolSpecification)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.ToolSpecifications.Update(toolSpecification);
                return await context.SaveChangesAsync();
            }

        }
    }
}
