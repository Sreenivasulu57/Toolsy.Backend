using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace VSC.Toolsy.Common.Models.CoreEntites
{
    [Table("RefreshToken")]
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }

        public required string Token { get; set; }

        public required DateTime ExpiresAt { get; set; }

        [Required]
        public bool IsRevoked { get; set; } = false;

        public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? RevokedAt { get; set; }


        [JsonIgnore]
        public Profile? profile { get; set; }

        public required Guid ProfileId { get; set; }
    }
}
