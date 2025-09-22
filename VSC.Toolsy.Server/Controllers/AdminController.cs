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
        public async Task<IActionResult> GetAllUsers()
        {
            List<AdminUserDto> users = await _adminService.GetAllUsersForAdminAsync();
            return Ok(ApiResponse<List<AdminUserDto>>.SuccessResponse(
                users,
                "Successfully fetched all users for the admin."
            ));
        }

        [HttpGet("users/verified")]
        public async Task<IActionResult> GetAllVerifiedUsers()
        {
            List<AdminUserDto> users = (await _adminService.GetAllUsersForAdminAsync())
                .Where(u => u.VerificationStatus.Equals(VerificationStatus.Verified))
                .ToList();

            return Ok(ApiResponse<List<AdminUserDto>>.SuccessResponse(
                users,
                "Successfully fetched all users for the admin."
            ));
        }

        [HttpGet("users/unVerified")]
        public async Task<IActionResult> GetAllUnVerifiedUsers()
        {
            List<AdminUserDto> users = (await _adminService.GetAllUsersForAdminAsync())
                .Where(u => u.VerificationStatus.Equals(VerificationStatus.Pending))
                .ToList();

            return Ok(ApiResponse<List<AdminUserDto>>.SuccessResponse(
                users,
                "Successfully fetched all users for the admin."
            ));
        }

        [HttpGet("users/deleted")]
        public async Task<IActionResult> GetAllDeletedUsers()
        {

            List<AdminUserDto> users = (await _adminService.GetAllUsersForAdminAsync())
                .Where(u => u.IsDeleted)
                .ToList();

            return Ok(ApiResponse<List<AdminUserDto>>.SuccessResponse(
                users,
                "Successfully fetched all users for the admin."
            ));

        }

        [HttpPut("profiles/approve")]
        public async Task<IActionResult> ApproveProfileAccount(string email)
        {

            bool result = await _adminService.ApproveProfileAccountAsync(email);

            return Ok(ApiResponse<bool>.SuccessResponse(result, "Approve Account"));

        }

        [HttpPut("users/deleteByEmail")]
        public async Task<IActionResult> DeleteUserAccount(string email)
        {

            bool result = await _adminService.DeleteUserAccountAsync(email);

            return Ok(ApiResponse<bool>.SuccessResponse(result, "Approve Account"));
        }

        [HttpGet("users/getByEmail")]
        public async Task<IActionResult> GetUserById(string email)
        {

            AdminUserDto adminUserDto = await _adminService.GetUserByEmailAsync(email);

            return Ok(ApiResponse<AdminUserDto>.SuccessResponse(adminUserDto, "GetUserByEmailAsync"));
        }
    }
}
