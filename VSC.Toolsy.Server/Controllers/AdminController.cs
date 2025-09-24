using Microsoft.AspNetCore.Mvc;
using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Enums;
using VSC.Toolsy.Common.Interfaces;

namespace VSC.Toolsy.Server.Controllers
{
    [ApiController]
    [Route("api/v1/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;

        }

        [HttpGet("users")]
        [ProducesResponseType(StatusCodes.Status200OK)] // OK - 200 status code 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code 
        public async Task<IActionResult> GetAllUsers()
        {
            List<AdminUserDto> users = await _adminService.GetAllUsersForAdminAsync();
            return Ok(ApiResponseDto<List<AdminUserDto>>.SuccessResponse(
                users,
                "Successfully fetched all users for the admin."
            ));
        }

        [HttpGet("users/verified")]
        [ProducesResponseType(StatusCodes.Status200OK)] // OK - 200 status code 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code 
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Not Found - 404 status code
        public async Task<IActionResult> GetAllVerifiedUsers()
        {
            List<AdminUserDto> users = (await _adminService.GetAllUsersForAdminAsync())
                .Where(u => u.VerificationStatus.Equals(VerificationStatus.Verified))
                .ToList();

            return Ok(ApiResponseDto<List<AdminUserDto>>.SuccessResponse(
                users,
                "Successfully fetched all users for the admin."
            ));

            if (users == null || users.Count == 0)
            {
                return NotFound(ApiResponseDto<string>.FailureResponse("No verified users found."));
            }
        }

        [HttpGet("users/unverified")]
        [ProducesResponseType(StatusCodes.Status200OK)] // OK - 200 status code 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code 
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Not Found - 404 status code
        public async Task<IActionResult> GetAllUnVerifiedUsers()
        {
            List<AdminUserDto> users = (await _adminService.GetAllUsersForAdminAsync())
                .Where(u => u.VerificationStatus.Equals(VerificationStatus.Pending))
                .ToList();

            return Ok(ApiResponseDto<List<AdminUserDto>>.SuccessResponse(
                users,
                "Successfully fetched all users for the admin."
            ));

            if (users == null || users.Count == 0)
            {
                return NotFound(ApiResponseDto<string>.FailureResponse("No Unverified users found."));
            }
        }

        [HttpGet("users/deleted")]
        [ProducesResponseType(StatusCodes.Status200OK)] // OK - 200 status code
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Not Found - 404 status code
        public async Task<IActionResult> GetAllDeletedUsers()
        {

            List<AdminUserDto> users = (await _adminService.GetAllUsersForAdminAsync())
                .Where(u => u.IsDeleted)
                .ToList();

            return Ok(ApiResponseDto<List<AdminUserDto>>.SuccessResponse(
                users,
                "Successfully fetched all users for the admin."
            ));

            if (users == null || users.Count == 0)
            {
                return NotFound(ApiResponseDto<string>.FailureResponse("No Deleted users found."));
            }

        }

        [HttpPut("profiles/approve")]
        [ProducesResponseType(StatusCodes.Status200OK)] // OK - 200 status code
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)] // NotAcceptable - 406 status code
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Not Found - 404 status code
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Bad Request - 400 status code
        public async Task<IActionResult> ApproveProfileAccount(string email)
        {

            bool result = await _adminService.ApproveProfileAccountAsync(email);

            return Ok(ApiResponseDto<bool>.SuccessResponse(result, "Approve Account"));

            if (!result)
            {
                return NotFound(ApiResponseDto<string>.FailureResponse("User not found or could not be approved."));
            }

            if (email == null)
            {
                return BadRequest(ApiResponseDto<string>.FailureResponse("Email cannot be empty"));
            }
        }

        [HttpPut("user/delete-by-email")]
        [ProducesResponseType(StatusCodes.Status200OK)] // OK - 200 status code
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)] // NotAcceptable - 406 status code
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Not Found - 404 status code
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Bad Request - 400 status code
        public async Task<IActionResult> DeleteUserAccount(string email)
        {

            bool result = await _adminService.DeleteUserAccountAsync(email);

            return Ok(ApiResponseDto<bool>.SuccessResponse(result, "Approve Account"));

            if (!result)
            {
                return NotFound(ApiResponseDto<string>.FailureResponse("User not found or could not be deleted."));
            }
            if (email == null)
            {
                return BadRequest(ApiResponseDto<string>.FailureResponse("Email cannot be empty"));
            }

        }

        [HttpGet("users/email")]
        [ProducesResponseType(StatusCodes.Status200OK)] // OK - 200 status code
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)] // NotAcceptable - 406 status code
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Internal Server Error - 500 status code
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Bad Request - 400 status code
        public async Task<IActionResult> GetUserByEmail(string email)
        {

            AdminUserDto adminUserDto = await _adminService.GetUserByEmailAsync(email);

            return Ok(ApiResponseDto<AdminUserDto>.SuccessResponse(adminUserDto, "GetUserByEmailAsync"));

            if (email == null)
            {
                return BadRequest(ApiResponseDto<string>.FailureResponse("Email cannot be empty"));
            }
        }
    }
}
