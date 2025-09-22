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
        public async Task<IActionResult> SaveUser([FromBody]RegisterUserDto dto)
        {

            if(dto==null)
            {
                return  BadRequest(ApiResponse<User>.FailureResponse("Enter the required data"));
            }

            User user=await _userService.SaveAsync(dto);

            return Ok(ApiResponse<User>.SuccessResponse(user, "Added Succesfully"));
        }
    }
}
