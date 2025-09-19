using Microsoft.AspNetCore.Mvc;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Server.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> getById(int id)
        {
            User userFromDb = await _userService.GetByIdAsync(id);
            return Ok(userFromDb);
        }

    }
}
