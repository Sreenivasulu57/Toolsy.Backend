using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VSC.Toolsy.Common.Constants;
using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Services;

namespace VSC.Toolsy.Server.Controllers
{
    [Route(RouteMap.ToolAvailability.Base)]
    [ApiController]
    public class ToolAvailabilityController : ControllerBase
    {
        private readonly IToolAvailabilityService _toolAvailabilityService;
        public ToolAvailabilityController(IToolAvailabilityService toolAvailabilityService)
        {
            _toolAvailabilityService = toolAvailabilityService;
        }

        [HttpPost(RouteMap.ToolAvailability.Save)]
        [ProducesResponseType(typeof(ApiResponseDto<ToolAvailability>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaveToolAvailability([FromBody] ToolAvailabilityRequestDto toolAvailabilityRequestDto)
        {

            if (toolAvailabilityRequestDto == null)
                return BadRequest(ApiResponseDto<string>.FailureResponse("Invalid request payload."));

            bool result = await _toolAvailabilityService.SaveToolAvailabilityAsync(toolAvailabilityRequestDto);

            if (result)
                return Ok(ApiResponseDto<ToolAvailability>.SuccessResponse(null, "ToolAvailability added successfully."));
            else
                return BadRequest(ApiResponseDto<string>.FailureResponse("Failed to add toolavailability."));

        }

        [HttpPut(RouteMap.ToolAvailability.Update)]
        [ProducesResponseType(typeof(ApiResponseDto<ToolAvailability>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateToolAvailability(Guid toolAvailabilityId, ToolAvailabilityRequestDto toolAvailabilityRequestDto)
        {

            if (toolAvailabilityRequestDto == null | toolAvailabilityId == Guid.Empty)
                return BadRequest(ApiResponseDto<string>.FailureResponse("Invalid request payload"));

            bool result = await _toolAvailabilityService.UpdateToolAvailabilityAsync(toolAvailabilityId, toolAvailabilityRequestDto);

            if (result)
                return Ok(ApiResponseDto<string>.SuccessResponse(null, "Updated successfully"));
            else
                return BadRequest(ApiResponseDto<string>.FailureResponse("failed to update the toolavailability"));

        }

        [HttpPut(RouteMap.ToolAvailability.Delete)]
        [ProducesResponseType(typeof(ApiResponseDto<ToolAvailability>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteToolAvailability(Guid toolAvailabilityId)
        {
            if (toolAvailabilityId == Guid.Empty)
                return BadRequest(ApiResponseDto<string>.FailureResponse("id shoudn't be null or empty"));

            bool result = await _toolAvailabilityService.DeleteByToolAvailabilityId(toolAvailabilityId);

            if (result)
                return Ok(ApiResponseDto<string>.SuccessResponse(null, "ToolSpecification deleted successfully"));
            else
                return BadRequest(ApiResponseDto<string>.FailureResponse($"Failed to delete the ToolAvailability with id {toolAvailabilityId}"));

        }

        [HttpGet(RouteMap.ToolAvailability.GetById)]
        [ProducesResponseType(typeof(ApiResponseDto<ToolAvailability>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByAvailabilityId(Guid toolAvailabilityId)
        {
            if (toolAvailabilityId == Guid.Empty)
                return BadRequest(ApiResponseDto<string>.FailureResponse("Id shouldn't be null or empty"));

            ToolAvailability  toolAvailabilityFromDb = await _toolAvailabilityService.GetByToolAvailabilityIdAsync(toolAvailabilityId);

            if (toolAvailabilityFromDb != null)
                return Ok(ApiResponseDto<ToolAvailability>.SuccessResponse(toolAvailabilityFromDb, "ToolCategory fetched successfully"));
            else
                return BadRequest(ApiResponseDto<string>.FailureResponse($"Toolcatgory with this id {toolAvailabilityId} is not present"));

        }

        [HttpGet(RouteMap.ToolAvailability.GetAll)]
        [ProducesResponseType(typeof(ApiResponseDto<List<ToolAvailability>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAvailability()
        {
            List<ToolAvailability> listOfToolAvailabilityFromDb = await _toolAvailabilityService.GetAllToolAvailabilityAsync();

            if (listOfToolAvailabilityFromDb == null || listOfToolAvailabilityFromDb.Count() == 0)
                return NotFound(ApiResponseDto<string>.FailureResponse("No tool availabilities found."));

            return Ok(ApiResponseDto<List<ToolAvailability>>.SuccessResponse(
                listOfToolAvailabilityFromDb,
                "List of tool availabilities fetched successfully"
            ));
        }

    }
}
