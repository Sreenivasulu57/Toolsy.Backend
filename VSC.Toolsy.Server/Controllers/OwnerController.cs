using Microsoft.AspNetCore.Mvc;
using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;


namespace VSC.Toolsy.Server.Controllers
{
    [Route("api/v1/owner")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;
        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterOwner([FromBody] OwnerRequestDto ownerRequestDto)
        {

            Owner ownerFromDb = await _ownerService.RegisterOwner(ownerRequestDto);

            return Ok(ApiResponseDto<Owner>.SuccessResponse(ownerFromDb, "Owner Account Created Successfully"));
        }

    }
}
