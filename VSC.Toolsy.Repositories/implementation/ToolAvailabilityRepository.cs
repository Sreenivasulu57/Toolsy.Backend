using Microsoft.EntityFrameworkCore;
using VSC.Toolsy.Common.Exceptions;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.Data;
using VSC.Toolsy.Repositories.Interfaces;

namespace VSC.Toolsy.Repositories.implementation
{
    public class ToolAvailabilityRepository : IToolAvailabilityRepository
    {

        public int Delete(ToolAvailability toolAvailability)
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                context.ToolAvailabilities.Remove(toolAvailability);
                return context.SaveChanges();
            }
        }

        public async  Task<int> DeleteAsync(ToolAvailability toolAvailability)
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                context.ToolAvailabilities.Remove(toolAvailability);
                return await context.SaveChangesAsync();
            }
        }

        public List<ToolAvailability> GetAllToolAvailability()
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.ToolAvailabilities.Where(ta => !ta.IsDeleted).ToList() ??
                    throw new ToolAvailabilityNotFoundException("ToolAvailabilities not found");
            }
        }

        public async Task<List<ToolAvailability>> GetAllToolAvailabilityAsync()
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.ToolAvailabilities.Where(ta => !ta.IsDeleted).ToListAsync() ??
                    throw new ToolAvailabilityNotFoundException("ToolAvailabilities not found");
            }
        }

        public ToolAvailability GetById(Guid toolAvailabilityId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.ToolAvailabilities.FirstOrDefault(ta => ta.Id.Equals(toolAvailabilityId) && !ta.IsDeleted ) ??
                     throw new ToolAvailabilityNotFoundException($"ToolAvailability not found with this ifd {toolAvailabilityId}");
            }
        }

        public async Task<ToolAvailability> GetByIdAsync(Guid toolAvailabilityId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.ToolAvailabilities.FirstOrDefaultAsync(ta => ta.Id.Equals(toolAvailabilityId) && !ta.IsDeleted) ??
                     throw new ToolAvailabilityNotFoundException($"ToolAvailability not found with id {toolAvailabilityId}");
            }
        }

        public int Save(ToolAvailability toolAvailability)
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                _context.ToolAvailabilities.Add(toolAvailability);
                return _context.SaveChanges();
            }
        }

        public async Task<int> SaveAsync(ToolAvailability toolAvailability)
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                await _context.ToolAvailabilities.AddAsync(toolAvailability);
                return await _context.SaveChangesAsync();
            }
        }

        public int Update(ToolAvailability toolAvailability)
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                _context.ToolAvailabilities.Update(toolAvailability);
                return _context.SaveChanges();
            }
        }

        public async Task<int> UpdateAsync(ToolAvailability toolAvailability)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.ToolAvailabilities.Update(toolAvailability);
                return await context.SaveChangesAsync();
            }
        }
    }
}
