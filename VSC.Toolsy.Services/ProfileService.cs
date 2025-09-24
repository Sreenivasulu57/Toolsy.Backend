using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.Interfaces;

namespace VSC.Toolsy.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<Profile> GetProfileByEmailAsync(string profileEmail)
        {
            Profile profile = await _profileRepository.GetByEmailAsync(profileEmail);

            return profile;
        }

        public async Task<Profile> GetProfileWithAddressByEmailAsync(string email)
            => await _profileRepository.GetProfileWithAddressByEmailAsync(email);
        
    }
}
