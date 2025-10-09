using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VSC.Toolsy.Common.Constants;
using VSC.Toolsy.Common.Enums;
using VSC.Toolsy.Common.Interfaces;

namespace VSC.Toolsy.Server.Controllers
{
    [ApiController]
    [Route(RouteMap.Email.Base)]
    [Authorize(policy: nameof(Policy.AUTHENTICATED_PROFILE))]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {

            _emailService = emailService;

        }

        [HttpPost(RouteMap.Email.SendOtp)]
        public async Task<IActionResult> SendOtp([FromQuery] string email)
        {

            bool result = await _emailService.SendEmailAsync(email);

            return Ok(result);

        }

        [HttpGet(RouteMap.Email.VerifyOtp)]
        public async Task<IActionResult> VerifyOtp([FromQuery] string email, [FromQuery] string otp)
        {

            bool result = await _emailService.VerifyOtpAsync(email, otp);

            return Ok(result);

        }
    }
}
