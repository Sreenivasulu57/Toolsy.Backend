using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VSC.Toolsy.Common.Constants;
using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Services;

namespace VSC.Toolsy.Server.Controllers
{
    [ApiController]
    [Route(RouteMap.Auth.Base)]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost(RouteMap.Auth.Login)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status200OK)] 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status401Unauthorized)] 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status403Forbidden)] 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)] 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]  
        public async Task<IActionResult> ProfileLogin([FromBody] LoginRequestDto loginRequestDto)
        {

            string jwtToken = await _authService.ProfileLoginAsync(loginRequestDto);

            return Ok(ApiResponseDto<string>.SuccessResponse(jwtToken, "Logged in Successfully"));

        }


        [HttpPost(RouteMap.Auth.Refresh)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status200OK)] 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status401Unauthorized)] 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status403Forbidden)] 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)] 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)] 
        public async Task<IActionResult> RefreshTokenAsync()
        {
            string newJwtToken = await _authService.RefreshTokenAsync();

            return Ok(ApiResponseDto<string>.SuccessResponse(newJwtToken, "New jwt generated Successfully"));

        }

        [HttpPost(RouteMap.Auth.Logout)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status200OK)] 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status401Unauthorized)] 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status403Forbidden)] 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)] 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]  
        public async Task<IActionResult> LogOutAsync()
        {

            bool statusOfLogOut = await _authService.LogoutAsync();

            if (statusOfLogOut)
                return Ok(ApiResponseDto<string>.SuccessResponse(null, "Logged out Successfully"));
            else
                return NotFound(ApiResponseDto<string>.FailureResponse("Failed to logout"));

        }
    }
}

