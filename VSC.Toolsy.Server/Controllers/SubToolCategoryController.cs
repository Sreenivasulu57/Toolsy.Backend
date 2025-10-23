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
    [Route(RouteMap.SubToolcategory.Base)]
    [ApiController]
    public class SubToolCategoryController : ControllerBase
    {
        private readonly IToolCategoryService _toolCategoryService;

        public SubToolCategoryController(IToolCategoryService toolCategoryService)
        {
            _toolCategoryService = toolCategoryService;
        }

        [HttpPost(RouteMap.SubToolcategory.Save)]
        [ProducesResponseType(typeof(ApiResponseDto<ToolCategory>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaveSubToolCategory(SubToolCategoryRequestDto subToolCategoryRequestDto)
        {
            if (subToolCategoryRequestDto == null)
                return BadRequest(ApiResponseDto<string>.FailureResponse("Invalid request payload."));

            bool result = await _toolCategoryService.SaveSubToolCategoryAsync(subToolCategoryRequestDto);

            if (result)
                return Ok(ApiResponseDto<ToolCategory>.SuccessResponse(null, "SubToolCategory added successfully."));
            else
                return BadRequest(ApiResponseDto<string>.FailureResponse("Failed to add SubToolCategory."));

        }

        [HttpPut(RouteMap.SubToolcategory.Update)]
        [ProducesResponseType(typeof(ApiResponseDto<ToolCategory>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSubToolCategory(Guid subToolCategoryId, [FromBody] UpdateSubToolCategoryRequestDto updateSubToolCategoryRequestDto)
        {

            if (updateSubToolCategoryRequestDto == null)
                return BadRequest(ApiResponseDto<string>.FailureResponse("Invalid request payload"));

            bool result = await _toolCategoryService.UpdateSubToolCategoryAsync(subToolCategoryId, updateSubToolCategoryRequestDto);

            if (result)
                return Ok(ApiResponseDto<string>.SuccessResponse(null, "Updated successfully"));
            else
                return BadRequest(ApiResponseDto<string>.FailureResponse("failed to update the SubToolCategory"));

        }

        [HttpPut(RouteMap.SubToolcategory.Delete)]
        [ProducesResponseType(typeof(ApiResponseDto<ToolCategory>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSubToolCategory(Guid subToolCategoryId)
        {
            if (subToolCategoryId == Guid.Empty)
                return BadRequest(ApiResponseDto<string>.FailureResponse("id shoudn't be null or empty"));

            bool result = await _toolCategoryService.DeleteSubToolCategoryByIdAsync(subToolCategoryId);

            if (result)
                return Ok(ApiResponseDto<string>.SuccessResponse(null, "SubToolCategory delted successfully"));
            else
                return BadRequest(ApiResponseDto<string>.FailureResponse($"Failed to delete the SubToolCategory with id {subToolCategoryId}"));

        }

        [HttpGet(RouteMap.SubToolcategory.GetById)]
        [ProducesResponseType(typeof(ApiResponseDto<ToolCategory>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBySubToolCategoryId(Guid subToolCategoryId)
        {
            if (subToolCategoryId == Guid.Empty)
                return BadRequest(ApiResponseDto<string>.FailureResponse("Id shouldn't be null or empty"));

            ToolCategory subToolCategoryFromDb = await _toolCategoryService.GetBySubToolCategoryId(subToolCategoryId);

            if (subToolCategoryFromDb != null)
                return Ok(ApiResponseDto<ToolCategory>.SuccessResponse(subToolCategoryFromDb, "SubToolCategory fetched successfully"));
            else
                return BadRequest(ApiResponseDto<string>.FailureResponse($"Toolcatgory with this id {subToolCategoryId} is not present"));

        }

        [HttpGet(RouteMap.SubToolcategory.GetAll)]
        [ProducesResponseType(typeof(ApiResponseDto<List<SubToolCategoryResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllToolCategory()
        {
            List<SubToolCategoryResponseDto> listOfToolCategoryFromDb = await _toolCategoryService.GetAllSubToolCategory();

            if (listOfToolCategoryFromDb == null || listOfToolCategoryFromDb.Count() == 0)
                return NotFound(ApiResponseDto<string>.FailureResponse("No subtoolcategories found."));

            return Ok(ApiResponseDto<List<SubToolCategoryResponseDto>>.SuccessResponse(
                listOfToolCategoryFromDb,
                "List of subtoolcategories fetched successfully"
            ));
        }

    }
}
