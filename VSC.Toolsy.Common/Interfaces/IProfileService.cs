using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Common.Interfaces
{
    public interface IProfileService
    {
        public Task<Profile> GetProfileByEmailAsync(string ProfileEmail);
    }
}
