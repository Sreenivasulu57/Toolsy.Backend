using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VSC.Toolsy.Common.Constants;
using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Enums;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Server.Controllers
{
    [Route(RouteMap.User.Base)]
    [ApiController]
    [Authorize(policy: nameof(Policy.USER_ONLY))]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost(RouteMap.User.Save)]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponseDto<Profile>), StatusCodes.Status200OK)] 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)] 
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)] 
        public async Task<IActionResult> SaveUser([FromBody] RegisterUserDto dto)
        {

            if (dto == null)
            {
                return BadRequest(ApiResponseDto<Profile>.FailureResponse("Enter the required data"));
            }

            Profile userFromDb = await _userService.SaveAsync(dto);

            return Ok(ApiResponseDto<Profile>.SuccessResponse(null, "Added Succesfully"));
        }

        [HttpGet(RouteMap.User.ById)]
        [ProducesResponseType(typeof(ApiResponseDto<Profile>), StatusCodes.Status200OK)] 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)] 
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)] 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)] 
        public async Task<IActionResult> GetByProfileId()
        {

            string profileIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid)?.Value;

            _logger.LogInformation(profileIdClaim);

            if (string.IsNullOrWhiteSpace(profileIdClaim))
                return NotFound(ApiResponseDto<string>.FailureResponse("Profile ID not found in token."));

            UserResposeDto userDto = await _userService.getProfileById(profileIdClaim);

            if (userDto == null)
                return NotFound(ApiResponseDto<string>.FailureResponse("User not found."));

            return Ok(ApiResponseDto<UserResposeDto>.SuccessResponse(userDto, "User fetched successfully."));
        }

        [HttpPut(RouteMap.User.DeleteByProfileId)]
        [ProducesResponseType(typeof(ApiResponseDto<Profile>), StatusCodes.Status200OK)] 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)] 
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)] 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)] 
        public async Task<IActionResult> DeleteByProfileId(Guid profileId)
        {
            if (profileId == null)
            {
                return BadRequest(ApiResponseDto<string>.FailureResponse("profileId is required."));
            }
            Profile userFromDb = await _userService.DeleteUserByProfileId(profileId);


            if (userFromDb == null)
            {
                return NotFound(ApiResponseDto<string>.FailureResponse("User not found or could not be deleted."));
            }

            return Ok(ApiResponseDto<Profile>.SuccessResponse(userFromDb, " User deleted successfully"));
        }

        [HttpPut(RouteMap.User.Update)]
        [ProducesResponseType(typeof(ApiResponseDto<Profile>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDTO userUpdateDTO)
        {
            string profileIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid)?.Value;
            _logger.LogInformation(profileIdClaim);

            if (string.IsNullOrWhiteSpace(profileIdClaim))
                return NotFound(ApiResponseDto<string>.FailureResponse("Profile ID not found in token."));

            UserResposeDto userDto = await _userService.getProfileById(profileIdClaim);
            if (userDto == null)
                return NotFound(ApiResponseDto<string>.FailureResponse("User not found."));

            if (Guid.TryParse(profileIdClaim, out Guid profileGuid))
            {
                Profile userFromDb = await _userService.UpdateUser(userUpdateDTO, profileGuid);
                if (userFromDb != null)
                    return Ok(ApiResponseDto<string>.SuccessResponse(null, "User updated successfully"));
            }

            return BadRequest(ApiResponseDto<string>.FailureResponse("Failed to update user."));
        }


    }
}


