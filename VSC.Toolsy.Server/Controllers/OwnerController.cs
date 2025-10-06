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
    [Route(RouteMap.Owner.Base)]
    [ApiController]
    [Authorize(policy: nameof(Policy.OWNER_ONLY))]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;
        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpPost(RouteMap.Owner.Register)]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponseDto<Owner>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterOwner([FromBody] OwnerRequestDto ownerRequestDto)
        {
            if (ownerRequestDto == null)
            {
                return BadRequest(ApiResponseDto<Owner>.FailureResponse("Enter the required data"));
            }

            Owner ownerFromDb = await _ownerService.RegisterOwner(ownerRequestDto);

            return Ok(ApiResponseDto<Owner>.SuccessResponse(ownerFromDb, "Owner Account Created Successfully"));
        }

        [HttpGet(RouteMap.Owner.GetByEmail)]
        [ProducesResponseType(typeof(ApiResponseDto<Owner>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOwnerByEmail([FromQuery] string email)
        {

            OwnerResponseDto ownerResponseDto = await _ownerService.GetOwnerByEmailAsync(email);
            return Ok(ApiResponseDto<OwnerResponseDto>.SuccessResponse(ownerResponseDto, "Owner fetched successfully."));

        }

        [HttpGet(RouteMap.Owner.GetByOwnerId)]
        public async Task<IActionResult> GetOwnerByOwnerId([FromQuery] Guid ownerId)
        {

            OwnerResponseDto ownerResponseDto = await _ownerService.GetOwnerByOwnerId(ownerId);

            return Ok(ApiResponseDto<OwnerResponseDto>.SuccessResponse(ownerResponseDto, "Owner fetched successfully."));

        }

    }
}
