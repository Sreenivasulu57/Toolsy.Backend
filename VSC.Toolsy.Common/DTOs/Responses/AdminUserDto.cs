using VSC.Toolsy.Common.Enums;

namespace VSC.Toolsy.Common.DTOs.Responses
{
    public class AdminUserDto
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfileImageUrl { get; set; }
        public bool IsDeleted { get; set; }
        public AccountStatus Status { get; set; }
        public VerificationStatus VerificationStatus { get; set; }
        public DateTime? EmailVerifiedAt { get; set; }
        public DateTime? PhoneVerifiedAt { get; set; }

    }

}
