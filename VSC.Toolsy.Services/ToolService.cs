using Microsoft.EntityFrameworkCore;
using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Enums;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.Interfaces;

namespace VSC.Toolsy.Services
{
    public class ToolService : IToolService
    {
        private readonly IToolRepository _toolRepository;
        private readonly IOwnerRepository _ownerRepository;

        public ToolService(IToolRepository toolRepository, IOwnerService ownerService, IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
            _toolRepository = toolRepository;


        }

        //Todo Use TransactionManagement
        public async Task<Tool> Save(ToolRequestDto toolRequestDto)
        {
            Owner ownerFromDb = await _ownerRepository.GetByOwnerId(toolRequestDto.OwnerId);

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
                CreatedBy = UserRole.Owner.ToString(),
                CreatedAt = DateTime.UtcNow,
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
                ToolCategoryId = toolRequestDto.ToolCategoryId,
                OwnerId = ownerFromDb.Id
            };

            int result = await _toolRepository.SaveAsync(tool);

            if (result <= 0)
            {
                throw new Exception("Internal Server Error");
            }

            return tool;
        }
        //todo in future booking update the available status as rented 
        public async Task<Tool> UpdateByToolId(ToolRequestDto toolRequestDto, Guid toolId)
        {
            Tool toolFromDb = await _toolRepository.GetByToolId(toolId);

            if (toolFromDb == null)
            {
                throw new Exception("Tool not found.");
            }

            toolFromDb.Name = toolRequestDto.Name;
            toolFromDb.Description = toolRequestDto.Description;
            toolFromDb.Brand = toolRequestDto.Brand;
            toolFromDb.Model = toolRequestDto.Model;
            toolFromDb.SerialNumber = toolRequestDto.SerialNumber;
            toolFromDb.ManufactureYear = toolRequestDto.ManufactureYear;
            toolFromDb.Condition = toolRequestDto.Condition;
            toolFromDb.HourlyRate = toolRequestDto.HourlyRate;
            toolFromDb.DailyRate = toolRequestDto.DailyRate;
            toolFromDb.WeeklyRate = toolRequestDto.WeeklyRate;
            toolFromDb.MonthlyRate = toolRequestDto.MonthlyRate;
            toolFromDb.YearlyRate = toolRequestDto.YearlyRate;
            toolFromDb.SecurityDeposit = toolRequestDto.SecurityDeposit;
            toolFromDb.AvailabilityStatus = toolRequestDto.AvailabilityStatus;
            toolFromDb.RequiresOperator = toolRequestDto.RequiresOperator;
            toolFromDb.OperatorRequirements = toolRequestDto.OperatorRequirements;
            toolFromDb.SafetyInstructions = toolRequestDto.SafetyInstructions;
            toolFromDb.UpdatedBy = UserRole.Owner.ToString();
            toolFromDb.UpdatedAt = DateTime.UtcNow;
            toolFromDb.OwnerId = toolRequestDto.OwnerId;

            toolFromDb.ToolImages = toolRequestDto.Images
                .Select(dto => new ToolImage
                {
                    ImageUrl = dto.ImageUrl,
                    AltText = dto.Name,
                    IsPrimary = dto.IsPrimary,
                    ToolId = toolFromDb.Id
                })
                .ToList();

            int result = await _toolRepository.UpdateAsync(toolFromDb);

            if (result <= 0)
            {
                throw new Exception("Internal Server Error");
            }

            return toolFromDb;
        }

        public async Task<List<Tool>> GetAllTByOwnerId(Guid ownerId)
        {
            List<Tool> toolsFromDb = await _toolRepository.GetAllByOwnerId(ownerId);
            return toolsFromDb;
        }

        public async Task<Tool> DeleteByToolId(Guid toolId)
        {

            Tool toolFromDb = await _toolRepository.GetByToolId(toolId);

            toolFromDb.DeletedAt = DateTime.UtcNow;
            toolFromDb.DeletedBy = UserRole.Owner.ToString();
            toolFromDb.IsDeleted = true;

            int result = await _toolRepository.UpdateAsync(toolFromDb);

            if (result == 0) throw new Exception("Intenal server error");

            return toolFromDb;
        }

        public async Task<List<Tool>> GetAll()
            => await _toolRepository.GetAllWithImagesAsync();

        public async Task<ToolResponseDto> GetByIdAsync(string toolId)
        {
            if (!Guid.TryParse(toolId, out Guid toolGuid))
            {
                return null;
            }

            return await _toolRepository.GetByFullToolId(toolGuid);
        }

        public async Task<List<ToolResponseDto>> SearchTools(string query)
        {
            return await _toolRepository.SearchTools(query);
        }
    }
}
