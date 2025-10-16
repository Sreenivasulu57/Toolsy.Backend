using VSC.Toolsy.Common.Enums;

namespace VSC.Toolsy.Common.DTOs.Responses
{
    public class UserResposeDto
    {
        public required string profileId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        public required DateTime DateOfBirth { get; set; }

        public required Gender Gender { get; set; }

        public required string ProfileImageUrl { get; set; }

    }
}
