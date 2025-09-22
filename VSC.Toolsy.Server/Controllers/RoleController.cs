using Microsoft.AspNetCore.Mvc;
using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;


namespace VSC.Toolsy.Server.Controllers
{
    [Route("api/v1/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveRole([FromBody] RoleRequestDTO roleRequestDTO)
        {
            Role roleFromDb = await _roleService.SaveRoleAsync(roleRequestDTO);

            return Ok(roleFromDb);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {

            List<Role> rolesFromDb = await _roleService.GetAllRolesAsync();

            return Ok(rolesFromDb);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {

            Role roleFromDb = await _roleService.GetByIdAsync(id);

            return Ok(roleFromDb);

        }

        [HttpPut("deleteById/{id}")]
        public async Task<IActionResult> DeleteRoleById(int id)
        {

            Role roleFromDb = await _roleService.DeleteRoleById(id);

            return Ok(roleFromDb);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole([FromBody] RoleUpdateRequestDTO roleUpdateRequestDTO, int id)
        {

            if (!id.Equals(roleUpdateRequestDTO.RoleId))
            {
                return BadRequest($"Id {id} in the URL does not match Id {roleUpdateRequestDTO.RoleId} in the body.");
            }

            Role roleFromDb = await _roleService.UpdateRole(roleUpdateRequestDTO, id);

            return Ok(roleFromDb);
        }
    }
}
