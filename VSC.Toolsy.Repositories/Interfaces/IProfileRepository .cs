using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Repositories.Interfaces
{
    public interface IProfileRepository
    {

        int Save(Profile profile);
        Task<int> SaveAsync(Profile profile);
        List<Profile> GetAll();
        Task<List<Profile>> GetAllAsync();
        Task<List<Profile>> GetAllUserAsync();
        Task<Profile> GetByProfileId(Guid profileId);
        Task<Profile> GetByEmailAsync(string email);
        Task<Profile> GetProfileByPhoneNumberAsync(string phoneNumber);
        Task<Profile> GetProfileWithAddressByEmailAsync(string profileEmail);
        Task<Profile> GetProfileWithAddressByProfileId(Guid profileId);
        Task<int> UpdateAsync(Profile profile);
        Task<int> DeleteAsync(Profile profile);

    }
}
