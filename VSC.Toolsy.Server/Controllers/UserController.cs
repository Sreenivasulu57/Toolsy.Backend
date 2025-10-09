using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost(RouteMap.User.Save)]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponseDto<Profile>), StatusCodes.Status200OK)] // OK - 200 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)] // Bad Request - 400 status code
        public async Task<IActionResult> SaveUser([FromBody] RegisterUserDto dto)
        {

            if (dto == null)
            {
                return BadRequest(ApiResponseDto<Profile>.FailureResponse("Enter the required data"));
            }

            Profile userFromDb = await _userService.SaveAsync(dto);

            return Ok(ApiResponseDto<Profile>.SuccessResponse(null, "Added Succesfully"));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseDto<Profile>), StatusCodes.Status200OK)] // OK - 200 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)] // Bad Request - 400 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)] // Not Found - 404 status code
        public async Task<IActionResult> GetByProfileId(Guid profileId)
        {


            Profile userFromDb = await _userService.GetProfileWithAddressByProfileId(profileId);

            if (userFromDb == null)
            {
                return NotFound(ApiResponseDto<string>.FailureResponse("User not found."));
            }

            return Ok(ApiResponseDto<Profile>.SuccessResponse(userFromDb, "GetUserByEmail"));
        }

        [HttpPut(RouteMap.User.DeleteByProfileId)]
        [ProducesResponseType(typeof(ApiResponseDto<Profile>), StatusCodes.Status200OK)] // OK - 200 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status codecode
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)] // Bad Request - 400 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)] // Not Found - 404 status code

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
        [ProducesResponseType(typeof(ApiResponseDto<Profile>), StatusCodes.Status200OK)] // OK - 200 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)] // Bad Request - 400 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)] // Not Found - 404 status code
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDTO userUpdateDTO, Guid profileId)
        {

            if (userUpdateDTO == null || profileId == null)
            {
                return BadRequest(ApiResponseDto<string>.FailureResponse("Invalid input data."));
            }

            Profile userFromDb = await _userService.UpdateUser(userUpdateDTO, profileId);


            if (userFromDb == null)
            {
                return NotFound(ApiResponseDto<string>.FailureResponse("User not found or could not be updated."));
            }

            return Ok(ApiResponseDto<Profile>.SuccessResponse(userFromDb, "Updated User successfully"));

        }

    }
}
