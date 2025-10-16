using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.Data;
using VSC.Toolsy.Repositories.Interfaces;

namespace VSC.Toolsy.Repositories.implementation
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        public String GenerateRefreshToken()
        => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        public async Task<RefreshToken?> GetByProfileIdAsync(Guid profileId)
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                return await _context.RefreshTokens.FirstOrDefaultAsync(r => r.ProfileId == profileId);
            }
        }

        public bool VerifyRefreshToken(string providedRefreshToken, string hashedRefreshTokenFromDb)
            => BCrypt.Net.BCrypt.Verify(providedRefreshToken, hashedRefreshTokenFromDb);

        public async Task<int> SaveAsync(RefreshToken refreshToken)
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                await _context.RefreshTokens.AddAsync(refreshToken);

                return await _context.SaveChangesAsync();
            }
        }

        public async Task<int> UpdateAsync(RefreshToken refreshToken)
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                _context.RefreshTokens.Update(refreshToken);

                return await _context.SaveChangesAsync();
            }
        }

        public async Task<List<RefreshToken>> GetAllAsync()
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                return await _context.RefreshTokens.ToListAsync();
            }
        }

        public async Task<RefreshToken> GetByTokenAsync(string refreshToken)
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                List<RefreshToken> refreshTokensFromDb = await GetAllAsync();

                return refreshTokensFromDb.FirstOrDefault(t => BCrypt.Net.BCrypt.Verify(refreshToken, t.Token));
            }
        }
    }
}







