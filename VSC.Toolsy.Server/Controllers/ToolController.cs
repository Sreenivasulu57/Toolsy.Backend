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
    [ApiController]
    [Route(RouteMap.Tool.Base)]
    [Authorize(policy: nameof(Policy.OWNER_ONLY))]
    public class ToolController : ControllerBase
    {
        private readonly IToolService _toolService;
        public ToolController(IToolService toolService)
        {

            _toolService = toolService;

        }

        [HttpPost(RouteMap.Tool.Save)]
        [ProducesResponseType(typeof(ApiResponseDto<Tool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SaveTool([FromBody] ToolRequestDto toolRequestDto)
        {

            Tool tool = await _toolService.Save(toolRequestDto);

            return Ok(ApiResponseDto<Tool>.SuccessResponse(tool, "Tool Added Successfully"));

        }

        [HttpPut(RouteMap.Tool.UpdateByToolId)]
        [ProducesResponseType(typeof(ApiResponseDto<Tool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTool([FromBody] ToolRequestDto toolRequestDto, Guid toolId)
        {
            Tool tool = await _toolService.UpdateByToolId(toolRequestDto, toolId);

            return Ok(ApiResponseDto<Tool>.SuccessResponse(tool, "Tool Updated Succesfully"));
        }
        [HttpPut(RouteMap.Tool.DeleteByToolId)]
        [ProducesResponseType(typeof(ApiResponseDto<Tool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTool(Guid toolId)
        {
            Tool tool = await _toolService.DeleteByToolId(toolId);

            return Ok(ApiResponseDto<Tool>.SuccessResponse(tool, "Tool deleted Succesfully"));
        }

        [HttpGet(RouteMap.Tool.FetchAllByOwnerId)]
        [ProducesResponseType(typeof(ApiResponseDto<Tool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllToolsOwnerIdAsync(Guid ownerId)
        {
            List<Tool> tools = await _toolService.GetAllTByOwnerId(ownerId);

            return Ok(ApiResponseDto<List<Tool>>.SuccessResponse(tools, "Tools fetched succesfully"));

        }
        [HttpGet(RouteMap.Tool.FetchAll)]
        [Authorize(policy: nameof(Policy.AUTHENTICATED_PROFILE))]
        [ProducesResponseType(typeof(ApiResponseDto<Tool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTools()
        {
            List<Tool> tools = await _toolService.GetAll();

            return Ok(ApiResponseDto<List<Tool>>.SuccessResponse(tools, "Tools fetched succesfully"));

        }
    }
}
