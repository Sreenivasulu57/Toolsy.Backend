using Microsoft.AspNetCore.Mvc;
using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Server.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveUser([FromBody] RegisterUserDto dto)
        {

            if (dto == null)
            {
                return BadRequest(ApiResponse<Profile>.FailureResponse("Enter the required data"));
            }

            Profile userFromDb = await _userService.SaveAsync(dto);

            return Ok(ApiResponse<Profile>.SuccessResponse(userFromDb, "Added Succesfully"));
        }

        [HttpGet("getByEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            Profile userFromDb = await _userService.GetByEmailAsync(email);

            return Ok(ApiResponse<Profile>.SuccessResponse(userFromDb, "GetUserByEmail"));
        }

        [HttpPut("deleteByEmail")]
        public async Task<IActionResult> DeleteUserByEmail(string email)
        {

            Profile userFromDb = await _userService.DeleteUserByEmailAsync(email);

            return Ok(ApiResponse<Profile>.SuccessResponse(userFromDb, "DeleteUserByEmail"));

        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDTO userUpdateDTO, string email)
        {

            Profile userFromDb = await _userService.UpdateUser(userUpdateDTO, email);

            return Ok(ApiResponse<Profile>.SuccessResponse(userFromDb, "UpdateUser"));
        }

    }
}
