using VSC.Toolsy.Common.Enums;

namespace VSC.Toolsy.Common.DTOs.Requests
{
    public class LoginRequestDto
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required LoginType Type { get; set; }
        public required Role Role { get; set; }
    }
}
