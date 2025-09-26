using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Enums;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Server.Controllers
{
    [ApiController]
    [Route("api/v1/tool")]
    [Authorize(policy: nameof(Policy.OWNER_ONLY))]
    public class ToolController : ControllerBase
    {
        private readonly IToolService _toolService;
        public ToolController(IToolService toolService)
        {

            _toolService = toolService;

        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseDto<Tool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SaveTool([FromBody] ToolRequestDto toolRequestDto)
        {

            Tool toolId = await _toolService.SaveToolAsync(toolRequestDto);

            return Ok(ApiResponseDto<Tool>.SuccessResponse(toolId, "Tool Added Successfully"));

        }
    }
}
