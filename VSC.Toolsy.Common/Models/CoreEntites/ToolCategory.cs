using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VSC.Toolsy.Common.Models.BaseEntites;

namespace VSC.Toolsy.Common.Models.CoreEntites
{
    [Table(name: "ToolCategory")]
    public class ToolCategory: AuditableEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? IconUrl { get; set; }

        public bool IsActive { get; set; } = true;

        public Guid? ParentCategoryId { get; set; }

        [JsonIgnore]
        public  ToolCategory? ParentCategory { get; set; }

        [JsonIgnore]
        public  List<ToolCategory>? SubCategories { get; set; } = new List<ToolCategory>();

        [JsonIgnore]
        public  List<Tool>? Tools { get; set; } = new List<Tool>();
    }
}
