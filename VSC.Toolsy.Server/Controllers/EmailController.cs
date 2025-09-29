using Microsoft.AspNetCore.Mvc;
using VSC.Toolsy.Common.Interfaces;

namespace VSC.Toolsy.Server.Controllers
{
    [ApiController]
    [Route("email")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {

            _emailService = emailService;

        }

        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOtp([FromQuery] string email)
        {

            bool result = await _emailService.SendEmailAsync(email);

            return Ok(result);

        }

        [HttpGet("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromQuery] string email, [FromQuery] string otp)
        {

            bool result = await _emailService.VerifyOtpAsync(email, otp);

            return Ok(result);

        }
    }
}
