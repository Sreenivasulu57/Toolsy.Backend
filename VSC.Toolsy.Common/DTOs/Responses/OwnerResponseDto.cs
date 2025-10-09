using System.ComponentModel.DataAnnotations;
using VSC.Toolsy.Common.Enums;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Common.DTOs.Responses
{
    public class OwnerResponseDto
    {
        public Guid ProfileId;

        [Required(ErrorMessage = "FirstName is required")]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "LastName is required")]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "PhoneNo is required")]
        [Phone]
        [StringLength(10)]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "DateOfBirth is required")]
        public required DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public required Gender Gender { get; set; }

        [Required(ErrorMessage = "ProfileImg is required")]
        public required string ProfileImageUrl { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        public AccountStatus Status { get; set; } = AccountStatus.Pending;

        [Required]
        public VerificationStatus VerificationStatus { get; set; } = VerificationStatus.Pending;

        public DateTime? EmailVerifiedAt { get; set; }

        public DateTime? PhoneVerifiedAt { get; set; }

        public List<UserRole> Roles { get; set; } = new List<UserRole>();

        public Address? Address { get; set; }


        public Guid OwnerId { get; set; }

        [StringLength(200)]
        public string? BusinessName { get; set; }

        [StringLength(500)]
        public string? BusinessDescription { get; set; }

        [StringLength(50)]
        public string? BusinessRegistrationNumber { get; set; }

    }
}
