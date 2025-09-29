using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Common.Interfaces
{
    public interface IProfileService
    {
        public Task<Profile> GetProfileByEmailAsync(string ProfileEmail);
        Task<Profile> GetProfileWithAddressByEmailAsync(string email);
        Task<string> ProfileLoginAsync(LoginRequestDto loginRequestDto);
        public Task<Profile> GetByProfileId(Guid ProfileId);
        Task<Profile> GetProfileWithAddressByProfileId(Guid ProfileId);
    }
}
