using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Repositories.Interfaces
{
    public interface IProfileRepository : IRepository<Profile>
    {
        Task<List<Profile>> GetAllUserAsync();
        Task<Profile> GetByEmailAsync(string email);
        Task<Profile> GetProfileByPhoneNumberAsync(string phoneNumber);
        Task<Profile> GetProfileWithAddressByEmailAsync(string profileEmail);
        Task<Profile> GetByProfileId(Guid profileId);
        Task<Profile> GetProfileWithAddressByProfileId(Guid profileId);
    }
}
