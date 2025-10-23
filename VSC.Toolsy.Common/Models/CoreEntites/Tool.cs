using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VSC.Toolsy.Common.Models.BaseEntites;
using VSC.Toolsy.Common.Enums;
using Newtonsoft.Json;


namespace VSC.Toolsy.Common.Models.CoreEntites
{
    [Table(name: "Tool")]
    public class Tool : AuditableEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [StringLength(100)]
        public string? Brand { get; set; }

        [StringLength(100)]
        public string? Model { get; set; }

        [StringLength(50)]
        public string? SerialNumber { get; set; }

        public int? ManufactureYear { get; set; }

        [Required]
        public ToolCondition Condition { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal HourlyRate { get; set; } = 0;

        [Column(TypeName = "decimal(10, 2)")]
        public decimal DailyRate { get; set; } = 0;

        [Column(TypeName = "decimal(10, 2)")]
        public decimal WeeklyRate { get; set; } = 0;

        [Column(TypeName = "decimal(10, 2)")]
        public decimal MonthlyRate { get; set; } = 0;

        [Column(TypeName = "decimal(10, 2)")]
        public decimal YearlyRate { get; set; } = 0;

        [Column(TypeName = "decimal(10, 2)")]
        public decimal SecurityDeposit { get; set; } = 0;

        [Required]
        public ToolAvailabilityStatus AvailabilityStatus { get; set; } = ToolAvailabilityStatus.Available;

        public bool RequiresOperator { get; set; } = false;

        public string? OperatorRequirements { get; set; }

        public string? SafetyInstructions { get; set; }


        public required Guid OwnerId { get; set; }

        public required  Guid ToolCategoryId { get; set; }

        [JsonIgnore]
        public  ToolCategory ToolCategory { get; set; }

        public List<ToolImage> ToolImages { get; set; } = new List<ToolImage>();

        public List<ToolSpecification> ToolSpecifications { get; set; } = new List<ToolSpecification>();

        public List<ToolAvailability> ToolAvailabilities { get; set; } = new List<ToolAvailability>();

    }
}
