using Microsoft.EntityFrameworkCore;
using VSC.Toolsy.Common.Exceptions;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.Data;
using VSC.Toolsy.Repositories.Interfaces;
using VSC.Toolsy.Common.DTOs.Responses;

namespace VSC.Toolsy.Repositories.implementation
{
    public class ToolCategoryRepository : IToolCategoryRepository
    {
        public int Delete(ToolCategory toolCategory)
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                 context.ToolCategories.Remove(toolCategory);
                return context.SaveChanges();
            }
        }

        public Task<int> DeleteAsync(ToolCategory toolCategory)
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                context.ToolCategories.Remove(toolCategory);
               return  context.SaveChangesAsync();
            }
        }

        public async Task<List<ToolCategoryResponseDto>> GetAllToolCategoryAsync()
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.ToolCategories.Where(tc => !tc.IsDeleted && tc.ParentCategoryId == null)
                    .Select(tc => new ToolCategoryResponseDto
                    {
                        ToolCategoryId = tc.Id,
                        Name = tc.Name,
                        Description = tc.Description,
                        IconUrl = tc.IconUrl,
                        SubCategories = tc.SubCategories
                        .Where(sc => !sc.IsDeleted)
                        .Select(sc => new SubToolCategoryResponseDto
                        {
                            SubToolCategoryId = sc.Id,
                            Name = sc.Name,
                            Description = sc.Description,
                            IconUrl = sc.IconUrl,
                            IsActive = sc.IsActive,
                            ParentCategoryId = sc.ParentCategoryId,
                            Tools = sc.Tools
                            .Where(t => !t.IsDeleted)
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
                                .Where(ta => !ta.IsDeleted)
                                .Select(ta => new ToolAvailabilityResponseDto
                                {
                                    ToolAvailabilityId = ta.Id,
                                    Date = ta.Date,
                                    StartTime = ta.StartTime,
                                    EndTime = ta.EndTime,
                                    IsAvailable = ta.IsAvailable,
                                    Notes = ta.Notes
                                }).ToList()

                            }).ToList()

                        }).ToList()

                    }).ToListAsync();
            }
        }
        public async Task<List<SubToolCategoryResponseDto>> GetAllSubToolCategoryAsync()
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.ToolCategories.Where(t => !t.IsDeleted && t.ParentCategoryId != null)
                    .Select(sc =>
                    new SubToolCategoryResponseDto
                    {
                        SubToolCategoryId = sc.Id,
                        Name = sc.Name,
                        Description = sc.Description,
                        IconUrl = sc.IconUrl,
                        IsActive = sc.IsActive,
                        ParentCategoryId = sc.ParentCategoryId,
                        Tools = sc.Tools
                        .Where(t => !t.IsDeleted)
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
                        }).ToList()
                    }).ToListAsync();
                       
            }
        }

        public  async Task<ToolCategory> GetByIdAsync(Guid toolCategoryId)
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
               return  await context.ToolCategories.FirstOrDefaultAsync(t => t.Id.Equals(toolCategoryId) && !t.IsDeleted )
                ?? throw new ToolCategoryNotFoundException("ToolCategory with id {toolCategoryId} is not found");
            }
        }

        public int Save(ToolCategory toolCategory)
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                context.ToolCategories.Add(toolCategory);
                return context.SaveChanges();
            }
        }

        public async Task<int> SaveAsync(ToolCategory toolCategory)
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                context.ToolCategories.AddAsync(toolCategory);
                return await context.SaveChangesAsync();
            }
        }

        public  int Update(ToolCategory toolCategory)
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                context.ToolCategories.Update(toolCategory);
                return  context.SaveChanges();
            }
        }

        public async Task<int> UpdateAsync(ToolCategory toolCategory)
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                context.ToolCategories.Update(toolCategory);
                return await context.SaveChangesAsync();
            }
        }

        public async Task<ToolCategory> GetToolCategoryByParentId(Guid parentCategoryId)
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
              return  await  context.ToolCategories.FirstOrDefaultAsync(t => t.Id.Equals(parentCategoryId) && !t.IsDeleted)
                    ?? throw new ToolCategoryNotFoundException($"Toolcategory not found with this id {parentCategoryId}");
            }
        }
    }
}
