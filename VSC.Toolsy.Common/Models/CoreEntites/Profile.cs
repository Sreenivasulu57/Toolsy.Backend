using System.ComponentModel.DataAnnotations;
using VSC.Toolsy.Common.Enums;
using VSC.Toolsy.Common.Models.BaseEntites;

namespace VSC.Toolsy.Common.Models.CoreEntites
{
    public class Profile : AuditableEntity
    {
        [Required(ErrorMessage ="FirstName is required")]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage ="LastName is required")]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage ="Email is required")]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage ="PhoneNo is required")]
        [Phone]
        [StringLength(10)]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage ="DateOfBirth is required")]
        public required DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage ="Gender is required")]
        public required Gender Gender { get; set; }

        [Required(ErrorMessage ="Password is required")]
        public required string PasswordHash { get; set; }

        [Required(ErrorMessage ="ProfileImg is required")]
        public required string ProfileImageUrl { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        public AccountStatus Status { get; set; } = AccountStatus.Pending;

        [Required]
        public VerificationStatus VerificationStatus { get; set; } = VerificationStatus.Pending;

        public DateTime? EmailVerifiedAt { get; set; }

        public DateTime? PhoneVerifiedAt { get; set; }

        public Role Role { get; set; } = Role.User;


        public Address? Address { get; set; }

    }
}
