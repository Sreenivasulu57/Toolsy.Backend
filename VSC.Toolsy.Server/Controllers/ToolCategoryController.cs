using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using VSC.Toolsy.Common.Constants;
using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Server.Controllers
{
    [Route(RouteMap.ToolCategory.Base)]
    [ApiController]
    public class ToolCategoryController : ControllerBase
    {
        private readonly IToolCategoryService _toolCategoryService;
        public ToolCategoryController(IToolCategoryService toolCategoryService)
        {
            _toolCategoryService = toolCategoryService;
        }

        [HttpPost(RouteMap.ToolCategory.Save)]
        [ProducesResponseType(typeof(ApiResponseDto<ToolCategory>), StatusCodes.Status200OK)] 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)] 
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)] 
        public async Task<IActionResult> SaveToolCategory([FromBody] ParentToolCategoryRequestDto parentToolCategoryRequestDto)
        {

            if (parentToolCategoryRequestDto == null)
                return BadRequest(ApiResponseDto<string>.FailureResponse("Invalid request payload."));

            bool result = await _toolCategoryService.SaveToolCategoryAsync(parentToolCategoryRequestDto);

            if (result)
                return Ok(ApiResponseDto<ToolCategory>.SuccessResponse(null, "Tool category added successfully."));
            else
                return  BadRequest(ApiResponseDto<string>.FailureResponse("Failed to add tool category."));

        }

        [HttpPut(RouteMap.ToolCategory.Update)]
        [ProducesResponseType(typeof(ApiResponseDto<ToolCategory>), StatusCodes.Status200OK)] 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)] 
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)] 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)] 
        public async Task<IActionResult> UpdateToolCategory(Guid toolCategoryId, [FromBody]UpdateParentToolCategoryRequestDto updateParentToolCategoryRequestDto)
        {

            if (updateParentToolCategoryRequestDto == null)
                return BadRequest(ApiResponseDto<string>.FailureResponse("Invalid request payload"));

            bool result = await _toolCategoryService.UpdateToolCategoryAsync(toolCategoryId,updateParentToolCategoryRequestDto);

            if (result)
                return Ok(ApiResponseDto<string>.SuccessResponse(null, "Updated successfully"));
            else
                return BadRequest(ApiResponseDto<string>.FailureResponse("failed to update the tool category"));

        }

        [HttpPut(RouteMap.ToolCategory.Delete)]
        [ProducesResponseType(typeof(ApiResponseDto<ToolCategory>), StatusCodes.Status200OK)] 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)] 
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)] 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)] 
        public async Task<IActionResult> DeleteToolCategory(Guid toolCategoryId)
        {
            if (toolCategoryId == Guid.Empty)
                return BadRequest(ApiResponseDto<string>.FailureResponse("id shoudn't be null or empty"));

            bool result = await _toolCategoryService.DeleteToolCategoryByIdAsync(toolCategoryId);

            if (result)
                return Ok(ApiResponseDto<string>.SuccessResponse(null, "ToolCategory deleted successfully"));
            else
                return BadRequest(ApiResponseDto<string>.FailureResponse($"Failed to delete the ToolCategory with id {toolCategoryId}"));

        }

        [HttpGet(RouteMap.ToolCategory.GetById)]
        [ProducesResponseType(typeof(ApiResponseDto<ToolCategory>), StatusCodes.Status200OK)] 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)] 
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)] 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByToolCategoryId(Guid toolCategoryId)
        {
            if (toolCategoryId == Guid.Empty)
                return BadRequest(ApiResponseDto<string>.FailureResponse("Id shouldn't be null or empty"));

            ToolCategory toolCategoryFromDb = await _toolCategoryService.GetByToolCategoryId(toolCategoryId);

            if (toolCategoryFromDb != null)
                return Ok(ApiResponseDto<ToolCategory>.SuccessResponse(toolCategoryFromDb, "ToolCategory fetched successfully"));
            else
                return BadRequest(ApiResponseDto<string>.FailureResponse($"Toolcatgory with this id {toolCategoryId} is not present"));

        }

        [HttpGet(RouteMap.ToolCategory.GetAll)]
        [ProducesResponseType(typeof(ApiResponseDto<List<ToolCategoryResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllToolCategory()
        {
            List<ToolCategoryResponseDto> listOfToolCategoryFromDb = await _toolCategoryService.GetAllToolCategory();

            if (listOfToolCategoryFromDb == null || listOfToolCategoryFromDb.Count() == 0)
                return NotFound(ApiResponseDto<string>.FailureResponse("No tool categories found."));

            return Ok(ApiResponseDto<List<ToolCategoryResponseDto>>.SuccessResponse(
                listOfToolCategoryFromDb,
                "List of tool categories fetched successfully"
            ));
        }
    }
}
