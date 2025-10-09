using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using VSC.Toolsy.Common.Constants;
using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Enums;
using VSC.Toolsy.Common.Interfaces;

namespace VSC.Toolsy.Server.Controllers
{
    [Route(RouteMap.Storage.Base)]
    [ApiController]
    [Authorize(policy: nameof(Policy.AUTHENTICATED_PROFILE))]
    public class StorageController : ControllerBase
    {
        private readonly IMediaService _mediaService;

        public StorageController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        [HttpPost(RouteMap.Storage.SaveFile)]
        public async Task<IActionResult> SaveFile(IFormFile file)
        {

            string originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Value.Trim('"');

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";

            string url = await _mediaService.SaveMediaAsync(file.OpenReadStream(), fileName, file.ContentType);

            return Ok(ApiResponseDto<string>.SuccessResponse(url, "Successfully Added File"));
        }

        [HttpGet(RouteMap.Storage.GetByFileName)]
        public IActionResult GetByFileName(string fileName)
        {

            string url = _mediaService.GetByFileName(fileName);

            return Ok(ApiResponseDto<string>.SuccessResponse(url, "Successfully Url Fetched"));
        }

        [HttpDelete(RouteMap.Storage.DeleteByFileName)]
        public async Task<IActionResult> DeleteByFileName(string fileName)
        {

            bool result = await _mediaService.DeleteByFileName(fileName);

            return Ok(ApiResponseDto<bool>.SuccessResponse(result, "Successfully Deleted File"));
        }

    }
}
