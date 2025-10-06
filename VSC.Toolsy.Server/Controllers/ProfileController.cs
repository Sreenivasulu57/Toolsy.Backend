using Microsoft.AspNetCore.Mvc;
using VSC.Toolsy.Common.Constants;
using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Interfaces;


namespace VSC.Toolsy.Server.Controllers
{
    [ApiController]
    [Route(RouteMap.Profile.Base)]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {

            _profileService = profileService;

        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status200OK)] // OK - 200 status code 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status401Unauthorized)] // Not Found - 404 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status403Forbidden)] // Not Found - 404 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)] // Not Found - 404 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code 
        public async Task<IActionResult> ProfileLogin([FromBody] LoginRequestDto loginRequestDto)
        {

            String accessToken = await _profileService.ProfileLoginAsync(loginRequestDto);

            return Ok(accessToken);

        }
    }
}
