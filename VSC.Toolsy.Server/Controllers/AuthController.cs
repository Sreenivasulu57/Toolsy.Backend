using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Services;

namespace VSC.Toolsy.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status200OK)] // OK - 200 status code 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status401Unauthorized)] // Not Found - 404 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status403Forbidden)] // Not Found - 404 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)] // Not Found - 404 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code 
        public async Task<IActionResult> ProfileLogin([FromBody] LoginRequestDto loginRequestDto)
        {

            TokenResponseDto tokenResponseDto = await _authService.ProfileLoginAsync(loginRequestDto);

            return Ok(ApiResponseDto<TokenResponseDto>.SuccessResponse(tokenResponseDto, "Logged in Successfully"));

        }

        [HttpPost("refresh")]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status200OK)] // OK - 200 status code 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status401Unauthorized)] // Not Found - 404 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status403Forbidden)] // Not Found - 404 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)] // Not Found - 404 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code 
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequestDTO refreshTokenRequestDTO)
        {

            TokenResponseDto tokenResponseDto = await _authService.RefreshTokenAsync(refreshTokenRequestDTO);

            return Ok(ApiResponseDto<TokenResponseDto>.SuccessResponse(tokenResponseDto, "New jwt generated Successfully"));

        }
    }
}
