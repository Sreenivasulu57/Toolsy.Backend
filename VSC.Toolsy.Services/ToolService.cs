using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.Enums;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.Interfaces;

namespace VSC.Toolsy.Services
{
    public class ToolService : IToolService
    {
        private readonly IToolRepository _toolRepository;
        private readonly IOwnerService _ownerService;

        public ToolService(IToolRepository toolRepository, IOwnerService ownerService)
        {

            _toolRepository = toolRepository;
            _ownerService = ownerService;

        }

        //Todo Use TransactionManagement
        public async Task<Tool> SaveToolAsync(ToolRequestDto toolRequestDto)
        {

            Owner ownerFromDb = await _ownerService.GetOwnerWithProfileByEmailAsync(toolRequestDto.OwnerEmail);

            List<ToolImage> toolImages = toolRequestDto.Images
                .Select(i => new ToolImage
                {
                    ImageUrl = i.ImageUrl,
                    AltText = i.Name,
                    IsPrimary = i.IsPrimary
                })
                .ToList();

            Tool tool = new Tool
            {
                Name = toolRequestDto.Name,
                Description = toolRequestDto.Description,
                Brand = toolRequestDto.Brand,
                Model = toolRequestDto.Model,
                SerialNumber = toolRequestDto.SerialNumber,
                ManufactureYear = toolRequestDto.ManufactureYear,
                Condition = toolRequestDto.Condition,
                HourlyRate = toolRequestDto.HourlyRate,
                DailyRate = toolRequestDto.DailyRate,
                WeeklyRate = toolRequestDto.WeeklyRate,
                MonthlyRate = toolRequestDto.MonthlyRate,
                YearlyRate = toolRequestDto.YearlyRate,
                SecurityDeposit = toolRequestDto.SecurityDeposit,
                RequiresOperator = toolRequestDto.RequiresOperator,
                OperatorRequirements = toolRequestDto.OperatorRequirements,
                SafetyInstructions = toolRequestDto.SafetyInstructions,
                ToolImages = toolImages,

                CreatedBy = Role.Owner.ToString(),
                CreatedAt = DateTime.UtcNow,

                OwnerId = ownerFromDb.Id
            };


            _toolRepository.Save(tool);

            int result = await _toolRepository.SaveChangesAsync();

            if (result <= 0)
            {
                throw new Exception("Internal Server Error");
            }

            return tool;
        }
    }
}
