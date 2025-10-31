using Microsoft.EntityFrameworkCore;
using VSC.Toolsy.Common.DTOs.Responses;
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

        public async Task<ToolResponseDto> GetByFullToolId(Guid toolId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Tools
                    .Where(t => !t.IsDeleted && t.Id.Equals(toolId))
                    .Select(t => new ToolResponseDto
                    {
                        ToolId = t.Id,
                        Name = t.Name,
                        Description = t.Description,
                        Brand = t.Brand,
                        Model = t.Model,
                        SerialNumber = t.SerialNumber,
                        ManufactureYear = t.ManufactureYear,
                        Condition = t.Condition,
                        HourlyRate = t.HourlyRate,
                        DailyRate = t.DailyRate,
                        WeeklyRate = t.WeeklyRate,
                        MonthlyRate = t.MonthlyRate,
                        YearlyRate = t.YearlyRate,
                        SecurityDeposit = t.SecurityDeposit,
                        AvailabilityStatus = t.AvailabilityStatus,
                        RequiresOperator = t.RequiresOperator,
                        OperatorRequirements = t.OperatorRequirements,
                        SafetyInstructions = t.SafetyInstructions,
                        ToolImages = t.ToolImages,
                        ToolSpecifications = t.ToolSpecifications
                            .Where(ts => !ts.IsDeleted)
                            .Select(ts => new ToolSpecificationResponseDto
                            {
                                ToolSpecificationId = ts.Id,
                                Name = ts.Name,
                                Value = ts.Value,
                                Unit = ts.Unit
                            }).ToList(),
                        ToolAvailabilities = t.ToolAvailabilities
                            .Where(t => !t.IsDeleted)
                            .Select(ta => new ToolAvailabilityResponseDto
                            {
                                ToolAvailabilityId = ta.Id,
                                Date = ta.Date,
                                StartTime = ta.StartTime,
                                EndTime = ta.EndTime,
                                IsAvailable = ta.IsAvailable,
                                Notes = ta.Notes
                            }).ToList()
                    }).FirstOrDefaultAsync()
                ?? throw new ToolNotFoundException($"tool not found with this id {toolId}");
            }
        }

        public async Task<Tool> GetByToolId(Guid toolId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Tools
                    .Where(t => !t.IsDeleted && t.Id.Equals(toolId))
                    .FirstOrDefaultAsync() ??
                    throw new ToolNotFoundException($"Tool with this id {toolId} not found");
            }
        }

        public async Task<List<ToolResponseDto>> SearchTools(string query)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Tools
                    .Where(t => !t.IsDeleted && t.Name.ToLower().Contains(query.ToLower()) ||
                   t.Brand.ToLower().Contains(query.ToLower()))
                   .Select(t => new ToolResponseDto
                   {
                       ToolId = t.Id,
                       Name = t.Name,
                       Description = t.Description,
                       Brand = t.Brand,
                       Model = t.Model,
                       SerialNumber = t.SerialNumber,
                       ManufactureYear = t.ManufactureYear,
                       Condition = t.Condition,
                       HourlyRate = t.HourlyRate,
                       DailyRate = t.DailyRate,
                       WeeklyRate = t.WeeklyRate,
                       MonthlyRate = t.MonthlyRate,
                       YearlyRate = t.YearlyRate,
                       SecurityDeposit = t.SecurityDeposit,
                       AvailabilityStatus = t.AvailabilityStatus,
                       RequiresOperator = t.RequiresOperator,
                       OperatorRequirements = t.OperatorRequirements,
                       SafetyInstructions = t.SafetyInstructions,
                       ToolImages = t.ToolImages,
                       ToolSpecifications = t.ToolSpecifications
                            .Where(ts => !ts.IsDeleted)
                            .Select(ts => new ToolSpecificationResponseDto
                            {
                                ToolSpecificationId = ts.Id,
                                Name = ts.Name,
                                Value = ts.Value,
                                Unit = ts.Unit
                            }).ToList(),
                       ToolAvailabilities = t.ToolAvailabilities
                            .Where(t => !t.IsDeleted)
                            .Select(ta => new ToolAvailabilityResponseDto
                            {
                                ToolAvailabilityId = ta.Id,
                                Date = ta.Date,
                                StartTime = ta.StartTime,
                                EndTime = ta.EndTime,
                                IsAvailable = ta.IsAvailable,
                                Notes = ta.Notes
                            }).ToList()
                   }).ToListAsync();
            }
        }
    }
}

