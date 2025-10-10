
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        String GenerateRefreshToken();

        bool VerifyRefreshToken(string providedRefreshToken, string hashedRefreshTokenFromDb);

        Task<int> SaveAsync(RefreshToken refreshToken);

        Task<int> UpdateAsync(RefreshToken refreshToken);

        Task<RefreshToken?> GetByProfileIdAsync(Guid profileId);
    }
}
