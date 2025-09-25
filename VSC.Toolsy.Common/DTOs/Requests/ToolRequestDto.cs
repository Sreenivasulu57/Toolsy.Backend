using System.ComponentModel.DataAnnotations;
using VSC.Toolsy.Common.Enums;

namespace VSC.Toolsy.Common.DTOs.Requests
{
    public class ToolRequestDto
    {
        [Required(ErrorMessage = "Owner email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string OwnerEmail { get; set; }

        [Required(ErrorMessage = "Tool name is required.")]
        [StringLength(200, ErrorMessage = "Name can't exceed 200 characters.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Description can't exceed 1000 characters.")]
        public string? Description { get; set; }

        [StringLength(100, ErrorMessage = "Brand can't exceed 100 characters.")]
        public string? Brand { get; set; }

        [StringLength(100, ErrorMessage = "Model can't exceed 100 characters.")]
        public string? Model { get; set; }

        [StringLength(50, ErrorMessage = "Serial number can't exceed 50 characters.")]
        public string? SerialNumber { get; set; }

        [Range(1900, 2100, ErrorMessage = "Manufacture year must be between 1900 and 2100.")]
        public int? ManufactureYear { get; set; }

        [Required]
        public ToolCondition Condition { get; set; }

        [Range(0, 9999999.99, ErrorMessage = "Hourly rate must be non-negative.")]
        public decimal HourlyRate { get; set; } = 0;

        [Range(0, 9999999.99, ErrorMessage = "Daily rate must be non-negative.")]
        public decimal DailyRate { get; set; } = 0;

        [Range(0, 9999999.99, ErrorMessage = "Weekly rate must be non-negative.")]
        public decimal WeeklyRate { get; set; } = 0;

        [Range(0, 9999999.99, ErrorMessage = "Monthly rate must be non-negative.")]
        public decimal MonthlyRate { get; set; } = 0;

        [Range(0, 9999999.99, ErrorMessage = "Yearly rate must be non-negative.")]
        public decimal YearlyRate { get; set; } = 0;

        [Range(0, 9999999.99, ErrorMessage = "Security deposit must be non-negative.")]
        public decimal SecurityDeposit { get; set; } = 0;

        public bool RequiresOperator { get; set; } = false;

        [StringLength(1000, ErrorMessage = "Operator requirements can't exceed 1000 characters.")]
        public string? OperatorRequirements { get; set; }

        [StringLength(1000, ErrorMessage = "Safety instructions can't exceed 1000 characters.")]
        public string? SafetyInstructions { get; set; }


        [Required(ErrorMessage = "At least one image is required.")]
        [MinLength(1, ErrorMessage = "At least one image must be provided.")]
        public required List<ToolImageRequestDto> Images { get; set; }
    }
}
