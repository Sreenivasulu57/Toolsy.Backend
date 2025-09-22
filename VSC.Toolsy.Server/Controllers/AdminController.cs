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
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;

        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            List<AdminUserDto> users = await _userService.GetAllUsersForAdminAsync();
            return Ok(ApiResponse<List<AdminUserDto>>.SuccessResponse(
                users,
                "Successfully fetched all users for the admin."
            ));
        }

        [HttpGet("users/verified")]
        public async Task<IActionResult> GetAllVerifiedUsers() {

            List<AdminUserDto> users = (await _userService.GetAllUsersForAdminAsync())
                .Where(u => u.VerificationStatus.Equals(VerificationStatus.Verified))
                .ToList();

            return Ok(ApiResponse<List<AdminUserDto>>.SuccessResponse(
                users,
                "Successfully fetched all users for the admin."
            ));

        }

        [HttpGet("users/unverified")]
        public async Task<IActionResult> GetAllUnverifiedUsers()
        {

            List<AdminUserDto> users = (await _userService.GetAllUsersForAdminAsync())
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

            List<AdminUserDto> users = (await _userService.GetAllUsersForAdminAsync())
                .Where(u => u.IsDeleted)
                .ToList();

            return Ok(ApiResponse<List<AdminUserDto>>.SuccessResponse(
                users,
                "Successfully fetched all users for the admin."
            ));

        }

        [HttpPut("users/{id}/approve")]
        public async Task<IActionResult> ApproveUserAccount(int id) {

            bool result = await _userService.ApproveUserAccountAsync(id);

            return Ok(ApiResponse<bool>.SuccessResponse(result, "Approve Account"));
            
        }
        [HttpPut("users/{id}/delete")]
        public async Task<IActionResult> DeleteUserAccount(int id) {

           bool result = await _userService.DeleteUserAccountAsync(id);

           return Ok(ApiResponse<bool>.SuccessResponse(result, "Approve Account"));
        }

        [HttpGet("/users/{id}")]
        public async Task<IActionResult> GetUserById(int id) {

            AdminUserDto adminUserDto =await  _userService.GetUserById(id);

            return Ok(ApiResponse<AdminUserDto>.SuccessResponse(adminUserDto, "GetUserById"));
        }
    }
}
