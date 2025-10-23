using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Common.Interfaces
{
    public interface IUserService
    {
        Task<Profile> DeleteUserByProfileId(Guid profileId);
        Task<List<Profile>> GetAllUserAsync();
        Task<Profile> GetByProfileId(Guid profileId);
        Task<Profile> SaveAsync(RegisterUserDto registerUserDto);
        Task<Profile> UpdateUser(UserUpdateDTO userUpdateDTO, Guid profileId);
        Task<int> UpdateUserAsync(Profile profile);
        Task<Profile> GetProfileWithAddressByProfileId(Guid profileId);
        Task<UserResposeDto> getProfileById(string profileIdString);
    }
}

