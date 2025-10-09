using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VSC.Toolsy.Common.Constants;
using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Enums;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Server.Controllers
{
    [ApiController]
    [Route(RouteMap.Admin.Base)]
    [Authorize(policy: nameof(Policy.ADMIN_ONLY))]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;

        }

        [HttpGet(RouteMap.Admin.GetAllUsers)]
        [ProducesResponseType(typeof(ApiResponseDto<Profile>), StatusCodes.Status200OK)] // OK - 200 status code 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)] // Not Found - 404 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code 
        public async Task<IActionResult> GetAllUsers()
        {
            List<AdminUserDto> users = await _adminService.GetAllUsersForAdminAsync();

            if (users == null)
            {
                return NotFound(ApiResponseDto<string>.FailureResponse("Users not found."));
            }
            return Ok(ApiResponseDto<List<AdminUserDto>>.SuccessResponse(
                users,
                "Successfully fetched all users for the admin."
            ));
        }

        [HttpGet(RouteMap.Admin.GetAllVerifiedUsers)]
        [ProducesResponseType(typeof(ApiResponseDto<Profile>), StatusCodes.Status200OK)] // OK - 200 status code 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)] // Not Found - 404 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code 
        public async Task<IActionResult> GetAllVerifiedUsers()
        {
            List<AdminUserDto> users = (await _adminService.GetAllUsersForAdminAsync())
                .Where(u => u.VerificationStatus.Equals(VerificationStatus.Verified))
                .ToList();

            if (users == null || users.Count == 0)
            {
                return NotFound(ApiResponseDto<string>.FailureResponse("No verified users found."));
            }

            return Ok(ApiResponseDto<List<AdminUserDto>>.SuccessResponse(
                users,
                "Successfully fetched all users for the admin."
            ));


        }

        [HttpGet(RouteMap.Admin.GetAllUnverifiedUsers)]
        [ProducesResponseType(typeof(ApiResponseDto<Profile>), StatusCodes.Status200OK)] // OK - 200 status code 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)] // Not Found - 404 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code 
        public async Task<IActionResult> GetAllUnVerifiedUsers()
        {
            List<AdminUserDto> users = (await _adminService.GetAllUsersForAdminAsync())
                .Where(u => u.VerificationStatus.Equals(VerificationStatus.Pending))
                .ToList();

            if (users == null || users.Count == 0)
            {
                return NotFound(ApiResponseDto<string>.FailureResponse("No Unverified users found."));
            }

            return Ok(ApiResponseDto<List<AdminUserDto>>.SuccessResponse(
                users,
                "Successfully fetched all users for the admin."
            ));


        }

        [HttpGet(RouteMap.Admin.GetAllDeletedUsers)]
        [ProducesResponseType(typeof(ApiResponseDto<Profile>), StatusCodes.Status200OK)] // OK - 200 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)] // Not Found - 404 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code
        public async Task<IActionResult> GetAllDeletedUsers()
        {

            List<AdminUserDto> users = (await _adminService.GetAllUsersForAdminAsync())
                .Where(u => u.IsDeleted)
                .ToList();

            if (users == null || users.Count == 0)
            {
                return NotFound(ApiResponseDto<string>.FailureResponse("No Deleted users found."));
            }

            return Ok(ApiResponseDto<List<AdminUserDto>>.SuccessResponse(
                users,
                "Successfully fetched all users for the admin."
            ));


        }

        [HttpPut(RouteMap.Admin.ApproveProfile)]
        [ProducesResponseType(typeof(ApiResponseDto<Profile>), StatusCodes.Status200OK)] // OK - 200 status code
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)] // Bad Request - 400 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)] // Not Found - 404 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code
        public async Task<IActionResult> ApproveProfileAccount(Guid profileId)
        {

            if (profileId == null)
            {
                return BadRequest(ApiResponseDto<string>.FailureResponse("ProfileId cannot be empty"));
            }

            bool result = await _adminService.ApproveProfileAccountAsync(profileId);

            if (!result)
            {
                return NotFound(ApiResponseDto<string>.FailureResponse("User not found."));
            }

            return Ok(ApiResponseDto<bool>.SuccessResponse(result, "Approve Account"));



        }

        [HttpPut(RouteMap.Admin.DeleteUser)]
        [ProducesResponseType(typeof(ApiResponseDto<Profile>), StatusCodes.Status200OK)] // OK - 200 status code
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)] // Bad Request - 400 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)] // Not Found - 404 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code
        public async Task<IActionResult> DeleteUserAccount(Guid profileId)
        {

            bool result = await _adminService.DeleteUserAccountAsync(profileId);

            if (!result)
            {
                return NotFound(ApiResponseDto<string>.FailureResponse("User not found or could not be deleted."));
            }

            return Ok(ApiResponseDto<bool>.SuccessResponse(result, "Approve Account"));

        }

        [HttpGet(RouteMap.Admin.GetUserByProfileId)]
        [ProducesResponseType(typeof(ApiResponseDto<Profile>), StatusCodes.Status200OK)] // OK - 200 status code
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)] // Bad Request - 400 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)] // Not Found - 404 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code
        public async Task<IActionResult> GetUserByProfileId(Guid profileId)
        {
            if (profileId == null)
            {
                return BadRequest(ApiResponseDto<string>.FailureResponse("ProfileId cannot be empty"));
            }

            AdminUserDto adminUserDto = await _adminService.GetUserByProfileIdAsync(profileId);


            if (adminUserDto == null)
            {
                return NotFound(ApiResponseDto<string>.FailureResponse("User not found."));
            }


            return Ok(ApiResponseDto<AdminUserDto>.SuccessResponse(adminUserDto, "GetUserByEmailAsync"));

        }
    }
}
