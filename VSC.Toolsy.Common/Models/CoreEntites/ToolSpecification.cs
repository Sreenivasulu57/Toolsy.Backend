using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using VSC.Toolsy.Common.Models.BaseEntites;

namespace VSC.Toolsy.Common.Models.CoreEntites
{
    public class ToolSpecification : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Value { get; set; } = string.Empty;

        [StringLength(50)]
        public string? Unit { get; set; }

        [JsonIgnore]
        public Tool Tool { get; set; } = null!;

        public required Guid ToolId { get; set; }
    }
}
