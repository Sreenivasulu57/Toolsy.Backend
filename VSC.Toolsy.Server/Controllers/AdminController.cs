using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Enums;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Server.Controllers
{
    [ApiController]
    [Route("api/v1/admin")]
    [Authorize(policy: nameof(Policy.ADMIN_ONLY))]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;

        }

        [HttpGet("users")]
        [ProducesResponseType(typeof(ApiResponseDto<Profile>), StatusCodes.Status200OK)] // OK - 200 status code 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)] // Not Found - 404 status code
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

        [HttpGet("users/verified")]
        [ProducesResponseType(typeof(ApiResponseDto<Profile>), StatusCodes.Status200OK)] // OK - 200 status code 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)] // Not Found - 404 status code
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

        [HttpGet("users/unverified")]
        [ProducesResponseType(typeof(ApiResponseDto<Profile>), StatusCodes.Status200OK)] // OK - 200 status code 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code 
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)] // Not Found - 404 status code
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

        [HttpGet("users/deleted")]
        [ProducesResponseType(typeof(ApiResponseDto<Profile>), StatusCodes.Status200OK)] // OK - 200 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)] // Not Found - 404 status code
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

        [HttpPut("profiles/approve-by-profileid")]
        [ProducesResponseType(typeof(ApiResponseDto<Profile>), StatusCodes.Status200OK)] // OK - 200 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)] // Not Found - 404 status code
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)] // Bad Request - 400 status code
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

        [HttpPut("user/delete-by-profileid")]
        [ProducesResponseType(typeof(ApiResponseDto<Profile>), StatusCodes.Status200OK)] // OK - 200 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)] // Not Found - 404 status code
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)] // Bad Request - 400 status code
        public async Task<IActionResult> DeleteUserAccount(Guid profileId)
        {

            bool result = await _adminService.DeleteUserAccountAsync(profileId);

            if (!result)
            {
                return NotFound(ApiResponseDto<string>.FailureResponse("User not found or could not be deleted."));
            }

            return Ok(ApiResponseDto<bool>.SuccessResponse(result, "Approve Account"));

        }

        [HttpGet("user/profileid")]
        [ProducesResponseType(typeof(ApiResponseDto<Profile>), StatusCodes.Status200OK)] // OK - 200 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)] // Bad Request - 400 status code
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)] // Not Found - 404 status code
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
