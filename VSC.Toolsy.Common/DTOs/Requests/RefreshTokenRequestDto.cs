
namespace VSC.Toolsy.Common.DTOs.Requests
{
    public class RefreshTokenRequestDTO
    {
        public required string RefreshToken { get; set; }

        public required Guid ProfileId { get; set; }
    }
}
