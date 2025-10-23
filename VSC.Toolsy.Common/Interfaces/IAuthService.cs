using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Common.Interfaces
{
    public interface IAuthService
    {
        Task<string> ProfileLoginAsync(LoginRequestDto loginRequestDto);

        Task<string> RefreshTokenAsync();

        Task<bool> LogoutAsync();
    }
}
