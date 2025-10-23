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
    [Route(RouteMap.ToolSpecification.Base)]
    [ApiController]
    public class ToolSpecificationController : ControllerBase
    {
        private readonly IToolSpecificationService _toolSpecificationService;
        public ToolSpecificationController(IToolSpecificationService toolSpecificationService)
        {
            _toolSpecificationService = toolSpecificationService;
        }

        [HttpPost(RouteMap.ToolSpecification.Save)]
        [ProducesResponseType(typeof(ApiResponseDto<ToolSpecification>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaveToolSpecification([FromBody]ToolSpecificationRequestDto toolSpecificationRequestDto)
        {

            if (toolSpecificationRequestDto == null)
                return BadRequest(ApiResponseDto<string>.FailureResponse("Invalid request payload."));

            bool result = await _toolSpecificationService.SaveToolSpecificationAsync(toolSpecificationRequestDto);

            if(result)
                return Ok(ApiResponseDto<ToolSpecification>.SuccessResponse(null, "ToolSpecification added successfully."));
            else
                return BadRequest(ApiResponseDto<string>.FailureResponse("Failed to add toolspecifiaction."));

        }

        [HttpPut(RouteMap.ToolSpecification.Update)]
        [ProducesResponseType(typeof(ApiResponseDto<ToolSpecification>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateToolSpecification(Guid toolSpecificationId, [FromBody] ToolSpecificationRequestDto toolSpecificationRequestDto)
        {

            if (toolSpecificationRequestDto == null)
                return BadRequest(ApiResponseDto<string>.FailureResponse("Invalid request payload"));

            bool result = await _toolSpecificationService.UpdateToolSpecificationAsync(toolSpecificationId, toolSpecificationRequestDto);

            if (result)
                return Ok(ApiResponseDto<string>.SuccessResponse(null, "Updated successfully"));
            else
                return BadRequest(ApiResponseDto<string>.FailureResponse("failed to update the toolspecification"));

        }

        [HttpPut(RouteMap.ToolSpecification.Delete)]
        [ProducesResponseType(typeof(ApiResponseDto<ToolSpecification>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteToolSpecification(Guid toolSpecificationId)
        {
            if (toolSpecificationId == Guid.Empty)
                return BadRequest(ApiResponseDto<string>.FailureResponse("id shoudn't be null or empty"));

            bool result = await _toolSpecificationService.DeleteToolSpecificationById(toolSpecificationId);

            if (result)
                return Ok(ApiResponseDto<string>.SuccessResponse(null, "ToolSpecification deleted successfully"));
            else
                return BadRequest(ApiResponseDto<string>.FailureResponse($"Failed to delete the ToolCategory with id {toolSpecificationId}"));

        }

        [HttpGet(RouteMap.ToolSpecification.GetById)]
        [ProducesResponseType(typeof(ApiResponseDto<ToolSpecification>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByToolSpecificationId(Guid toolSpecificationId)
        {
            if (toolSpecificationId == Guid.Empty)
                return BadRequest(ApiResponseDto<string>.FailureResponse("Id shouldn't be null or empty"));

            ToolSpecification toolSpecificationFromDb = await _toolSpecificationService.GetToolSpecificationById(toolSpecificationId);

            if (toolSpecificationFromDb != null)
                return Ok(ApiResponseDto<ToolSpecification>.SuccessResponse(toolSpecificationFromDb, "ToolCategory fetched successfully"));
            else
                return BadRequest(ApiResponseDto<string>.FailureResponse($"Toolcatgory with this id {toolSpecificationId} is not present"));

        }

        [HttpGet(RouteMap.ToolSpecification.GetAll)]
        [ProducesResponseType(typeof(ApiResponseDto<List<ToolSpecification>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllToolCategory()
        {
            List<ToolSpecification> listOfToolSpecificationFromDb = await _toolSpecificationService.GetAllToolSpecification();

            if (listOfToolSpecificationFromDb == null || listOfToolSpecificationFromDb.Count() == 0)
                return NotFound(ApiResponseDto<string>.FailureResponse("No tool categories found."));

            return Ok(ApiResponseDto<List<ToolSpecification>>.SuccessResponse(
                listOfToolSpecificationFromDb,
                "List of tool categories fetched successfully"
            ));
        }

    }
}
