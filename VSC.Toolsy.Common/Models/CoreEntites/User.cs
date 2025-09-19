using System.ComponentModel.DataAnnotations;
using VSC.Toolsy.Common.Models.BaseEntites;
using VSC.Toolsy.Common.Enums;

namespace VSC.Toolsy.Common.Models.CoreEntites
{
    public class User : AuditableEntity
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; } = string.Empty;

        [Phone]
        [StringLength(20)]
        public string? PhoneNumber { get; set; }

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public string? ProfileImageUrl { get; set; }

        [Required]
        public AccountStatus Status { get; set; } = AccountStatus.Pending;

        [Required]
        public VerificationStatus VerificationStatus { get; set; } = VerificationStatus.Pending;

        public DateTime? EmailVerifiedAt { get; set; }

        public DateTime? PhoneVerifiedAt { get; set; }

        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
