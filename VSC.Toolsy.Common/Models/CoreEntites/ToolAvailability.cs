using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VSC.Toolsy.Common.Models.BaseEntites;

namespace VSC.Toolsy.Common.Models.CoreEntites
{
    [Table(name:"ToolAvailability")]
    public class ToolAvailability : BaseEntity
    {
        [Required]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public bool IsAvailable { get; set; } = true;

        public string? Notes { get; set; }

        [JsonIgnore]
        public  Tool Tool { get; set; } = null!;
        public Guid ToolId { get; set; }
    }
}
