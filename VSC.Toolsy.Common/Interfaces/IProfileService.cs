using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Common.Interfaces
{
    public interface IProfileService
    {
        Task<Profile> GetProfileByEmailAsync(string ProfileEmail);
        Task<Profile> GetProfileWithAddressByEmailAsync(string email);
        Task<Profile> GetByProfileId(Guid ProfileId);
        Task<Profile> GetProfileWithAddressByProfileId(Guid ProfileId);
    }
}
