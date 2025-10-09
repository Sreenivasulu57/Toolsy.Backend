using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Common.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(string email, string subject, string body);
        Task<bool> SendEmailAsync(string email);
        Task SendRegistrationSuccessEmailAsync(Profile profile);
        Task<bool> VerifyOtpAsync(string email, string otp);
    }
}
