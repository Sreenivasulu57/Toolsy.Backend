using System.ComponentModel.DataAnnotations;

namespace VSC.Toolsy.Common.Models.BaseEntites
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = new Guid();

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime? DeletedAt { get; set; }
    }
}
