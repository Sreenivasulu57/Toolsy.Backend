using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace VSC.Toolsy.Common.Models.CoreEntites
{
    [Table(name: "Owner")]
    public class Owner
    {
        [Key]
        public Guid Id { get; set; } = new Guid();

        [StringLength(200)]
        public string? BusinessName { get; set; }

        [StringLength(500)]
        public string? BusinessDescription { get; set; }

        [StringLength(50)]
        public string? BusinessRegistrationNumber { get; set; }

        [JsonIgnore]
        public Profile? Profile { get; set; }

        [JsonIgnore]
        public required Guid ProfileId { get; set; }

        public List<Tool> Tools { get; set; } = new List<Tool>();
    }
}
