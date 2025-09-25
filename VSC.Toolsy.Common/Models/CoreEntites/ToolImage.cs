using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VSC.Toolsy.Common.Models.BaseEntites;

namespace VSC.Toolsy.Common.Models.CoreEntites
{
    [Table(name: "ToolImage")]
    public class ToolImage : BaseEntity
    {

        [Required(ErrorMessage = "ImageUrl is required")]
        public string ImageUrl { get; set; } = string.Empty;

        [StringLength(200)]
        public string? AltText { get; set; }

        public bool IsPrimary { get; set; } = false;

        [Required]
        [JsonIgnore]
        public Guid ToolId { get; set; }

    }
}
