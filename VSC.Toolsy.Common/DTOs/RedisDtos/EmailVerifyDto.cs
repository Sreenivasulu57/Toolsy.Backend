
namespace VSC.Toolsy.Common.DTOs.RedisDtos
{
    public class EmailVerifyDto
    {
        public required string Otp { get; set; }
        public required DateTime IssuedAt { get; set; }

    }
}
