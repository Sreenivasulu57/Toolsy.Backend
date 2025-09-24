using Microsoft.AspNetCore.Mvc;
using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Server.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService; 

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("save")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Bad Request - 400 status code
        [ProducesResponseType(StatusCodes.Status200OK)] // OK - 200 status code
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)] // Not Acceptable - 406 status code
        public async Task<IActionResult> SaveUser([FromBody] RegisterUserDto dto)
        {

            if (dto == null)
            {
                return BadRequest(ApiResponseDto<Profile>.FailureResponse("Enter the required data"));
            }

            Profile userFromDb = await _userService.SaveAsync(dto);

            return Ok(ApiResponseDto<Profile>.SuccessResponse(userFromDb, "Added Succesfully"));
        }

        [HttpGet("email")]
        [ProducesResponseType(StatusCodes.Status200OK)] // OK - 200 status code
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Not Found - 404 status code
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Bad Request - 400 status code
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            Profile userFromDb = await _userService.GetByEmailAsync(email);

            return Ok(ApiResponseDto<Profile>.SuccessResponse(userFromDb, "GetUserByEmail"));

            if(userFromDb == null)
            {
                return NotFound(ApiResponseDto<string>.FailureResponse("User not found."));
            }
            if(email == null)
            {
                return BadRequest(ApiResponseDto<string>.FailureResponse("Email is required."));
            }
        }

        [HttpPut("delete-user-by-email")]
        [ProducesResponseType(StatusCodes.Status200OK)] // OK - 200 status code
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Not Found - 404 status code
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Bad Request - 400 status code
        public async Task<IActionResult> DeleteUserByEmail(string email)
        {

            Profile userFromDb = await _userService.DeleteUserByEmailAsync(email);

            return Ok(ApiResponseDto<Profile>.SuccessResponse(userFromDb, "DeleteUserByEmail"));

            if (userFromDb == null)
            {
                return NotFound(ApiResponseDto<string>.FailureResponse("User not found or could not be deleted."));
            }
            if(email == null)
            {
                return BadRequest(ApiResponseDto<string>.FailureResponse("Email is required."));
            }

        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)] // OK - 200 status code
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Not Found - 404 status code
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Bad Request - 400 status code
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDTO userUpdateDTO, string email)
        {

            Profile userFromDb = await _userService.UpdateUser(userUpdateDTO, email);

            return Ok(ApiResponseDto<Profile>.SuccessResponse(userFromDb, "UpdateUser"));

            if (userFromDb == null)
            {
                return NotFound(ApiResponseDto<string>.FailureResponse("User not found or could not be updated."));
            }

            if (userUpdateDTO == null || email == null)
            {
                return BadRequest(ApiResponseDto<string>.FailureResponse("Invalid input data."));
            }
        }

    }
}
