using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using VSC.Toolsy.Common.DTOs.RedisDtos;
using VSC.Toolsy.Common.Exceptions;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.Interfaces;

namespace VSC.Toolsy.Services
{
    public class EmailService : IEmailService
    {
        #region Fields & Dependencies


        private readonly IConfiguration _configuration;
        private readonly IRedisCacheService _redisCacheService;

        private readonly string FROM_NAME;
        private readonly string FROM_ADDRESS;
        private readonly string PASSWORD;
        private readonly string HOST;
        private readonly int PORT;

        private readonly TimeSpan CACHE_EXPIRATION;
        private readonly int OTP_EXPIRATION;

        #endregion

        #region Constructor

        public EmailService(IConfiguration configuration, IRedisCacheService redisCacheService)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _redisCacheService = redisCacheService ?? throw new ArgumentNullException(nameof(redisCacheService));

            // Load email credentials
            FROM_NAME = _configuration["EmailCredentials:FromName"] ?? throw new Exception("FromName is missing in configuration");
            FROM_ADDRESS = _configuration["EmailCredentials:FromAddress"] ?? throw new Exception("FromAddress is missing in configuration");
            PASSWORD = _configuration["EmailCredentials:Password"] ?? throw new Exception("Password is missing in configuration");
            HOST = _configuration["EmailCredentials:Host"] ?? throw new Exception("Host is missing in configuration");
            PORT = Convert.ToInt32(_configuration["EmailCredentials:Port"] ?? throw new Exception("Port is missing in configuration"));

            // Load cache/OTP settings
            CACHE_EXPIRATION = TimeSpan.FromMinutes(Convert.ToInt32(_configuration["Redis:CacheExpiration"] ?? throw new Exception("CacheExpiration is missing")));
            OTP_EXPIRATION = Convert.ToInt32(_configuration["Otp:ExpirationTime"] ?? throw new Exception("Otp ExpirationTime is missing"));
        }

        #endregion

        #region Public Methods

        public void SendEmail(string recipientEmail, string subject, string messageBody)
        {
            MimeMessage message = BuildMimeMessage(recipientEmail, subject, messageBody);

            SmtpClient client = new SmtpClient();
            client.Connect(HOST, PORT, SecureSocketOptions.StartTls);
            client.Authenticate(FROM_ADDRESS, PASSWORD);
            client.Send(message);
            client.Disconnect(true);
        }

        public async Task<bool> SendEmailAsync(string recipientEmail)
        {
            var otp = GenerateOtp();

            var nowUtc = DateTime.UtcNow;
            var nowIst = ConvertToIst(nowUtc);

            var subject = $"Verification Code: {otp}";

            var messageBody = (await LoadTemplateAsync("otp_verification_email"))
                .Replace("{{OTP}}", otp)
                .Replace("{{ExpiresAt}}", FormatExpiryTime(nowUtc, nowIst))
                .Replace("{{SupportEmail}}", FROM_ADDRESS);

            var emailVerifyDto = new EmailVerifyDto
            {
                IssuedAt = nowUtc,
                Otp = otp
            };

            await _redisCacheService.SetCacheValueAsync(recipientEmail, emailVerifyDto, CACHE_EXPIRATION);

            SendEmail(recipientEmail, subject, messageBody);

            return true;
        }

        public async Task SendRegistrationSuccessEmailAsync(Profile profile)
        {
            string subject = $"Welcome! Your {profile.Roles} Account Has Been Successfully Created";

            string messageBody = (await LoadTemplateAsync("registration_success_email"))
                .Replace("{{FirstName}}", profile.FirstName)
                .Replace("{{Role}}", profile.Roles.ToString())
                .Replace("{{Email}}", profile.Email)
                .Replace("{{CompanyName}}", FROM_NAME)
                .Replace("{{SupportEmail}}", FROM_ADDRESS);

            SendEmail(profile.Email, subject, messageBody);
        }

        public async Task<bool> VerifyOtpAsync(string email, string otp)
        {
            var emailVerifyDto = await _redisCacheService.GetCacheValueAsync<EmailVerifyDto>(email)
                ?? throw new EmailNotFoundException($"No credentials saved for email: {email}");

            if (!emailVerifyDto.Otp.Equals(otp))
                throw new OtpException($"The provided OTP does not match: {otp}");

            if (emailVerifyDto.IssuedAt.AddMinutes(OTP_EXPIRATION) <= DateTime.UtcNow)
                throw new OtpExpiredException($"OTP has expired at {emailVerifyDto.IssuedAt.AddMinutes(OTP_EXPIRATION)}");

            return true;
        }

        #endregion

        #region Private Helpers

        private MimeMessage BuildMimeMessage(string recipientEmail, string subject, string messageBody)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(FROM_NAME, FROM_ADDRESS));
            message.To.Add(new MailboxAddress("", recipientEmail));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = messageBody };

            return message;
        }

        private string GenerateOtp() =>
            new Random().Next(100000, 999999).ToString();

        private DateTime ConvertToIst(DateTime utcTime)
        {
            var istZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utcTime, istZone);
        }

        private string FormatExpiryTime(DateTime utcTime, DateTime istTime)
        {
            return $"{utcTime.AddMinutes(OTP_EXPIRATION):HH:mm:ss} UTC / " +
                   $"{istTime.AddMinutes(OTP_EXPIRATION):HH:mm:ss} IST";
        }

        private static async Task<string> LoadTemplateAsync(string htmlFileName)
        {
            string filePath = Directory
                .GetCurrentDirectory()
                .Replace("VSC.Toolsy.Server", $@"\VSC.Toolsy.Common\Resource\{htmlFileName}.html");

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Template file not found at: {filePath}");

            return await File.ReadAllTextAsync(filePath);
        }

        #endregion
    }
}
