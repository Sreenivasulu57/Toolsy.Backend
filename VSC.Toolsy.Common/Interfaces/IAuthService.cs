using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Common.Interfaces
{
    public interface IAuthService
    {
        Task<TokenResponseDto> ProfileLoginAsync(LoginRequestDto loginRequestDto);

        Task<TokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDTO refreshTokenRequestDTO);
    }
}
