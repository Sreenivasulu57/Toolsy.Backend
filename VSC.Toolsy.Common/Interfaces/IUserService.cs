using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Common.Interfaces
{
    public interface IUserService
    {
        Task<Profile> DeleteUserByEmailAsync(string email);
        Task<List<Profile>> GetAllUserAsync();
        Task<Profile> GetByEmailAsync(string email);
        public Task<Profile> SaveAsync(RegisterUserDto registerUserDto);
        Task<Profile> UpdateUser(UserUpdateDTO userUpdateDTO, string email);
        Task<int> UpdateUserAsync(Profile profile);
        Task<Profile> GetProfileWithAddressByEmailAsync(string profileEmail);

    }
}
