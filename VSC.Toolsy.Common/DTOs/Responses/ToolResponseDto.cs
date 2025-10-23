using VSC.Toolsy.Common.Enums;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Common.DTOs.Responses
{
    public class ToolResponseDto
    {
        public Guid ToolId { get; set; }

        public required string Name { get; set; } = string.Empty;

        public required string Description { get; set; }

        public required string Brand { get; set; }

        public required string Model { get; set; }

        public required string SerialNumber { get; set; }

        public required int? ManufactureYear { get; set; }

        public ToolCondition Condition { get; set; }

        public required decimal HourlyRate { get; set; } = 0;

        public required decimal DailyRate { get; set; } = 0;

        public required decimal WeeklyRate { get; set; } = 0;

        public required decimal MonthlyRate { get; set; } = 0;

        public required decimal YearlyRate { get; set; } = 0;

        public required decimal SecurityDeposit { get; set; } = 0;

        public required ToolAvailabilityStatus AvailabilityStatus { get; set; } = ToolAvailabilityStatus.Available;

        public required bool RequiresOperator { get; set; } = false;

        public required string OperatorRequirements { get; set; }

        public required string SafetyInstructions { get; set; }

        public required List<ToolImage> ToolImages { get; set; } = new List<ToolImage>();

        public required List<ToolSpecificationResponseDto> ToolSpecifications { get; set; } = new List<ToolSpecificationResponseDto>();

        public required List<ToolAvailabilityResponseDto> ToolAvailabilities { get; set; } = new List<ToolAvailabilityResponseDto>();

    }
}

