using Microsoft.AspNetCore.Mvc;
using VSC.Toolsy.Common.Exceptions;

namespace VSC.Toolsy.Server.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpGet("getById/{id}")]
        public IActionResult getById(int id)
        {

            if (id > 10)
            {
                throw new UserNotFoundException($"thw user with this id {id} not found");
            }

            return Ok(id);

        }

    }
}
