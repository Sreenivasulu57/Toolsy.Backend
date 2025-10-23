using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Enums;
using VSC.Toolsy.Common.Exceptions;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.Interfaces;

namespace VSC.Toolsy.Services
{
    public class AuthService : IAuthService
    {
        private readonly IProfileService _profileService;
        private readonly IProfileRepository _profileRepository;
        private readonly ISigningKeyRepository _signingKeyRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IProfileService profileService, IProfileRepository profileRepository,
            ISigningKeyRepository signingKeyRepository, IConfiguration configuration,
            IRefreshTokenRepository refreshTokenRepository, IHttpContextAccessor httpContextAccessor)
        {
            _profileService = profileService;
            _profileRepository = profileRepository;
            _signingKeyRepository = signingKeyRepository;
            _configuration = configuration;
            _refreshTokenRepository = refreshTokenRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> ProfileLoginAsync(LoginRequestDto loginRequestDto)
        {
            Profile? profileFromDb = loginRequestDto.Type switch
            {
                LoginType.EMAIL => await _profileService.GetProfileByEmailAsync(loginRequestDto.UserName),
                LoginType.NUMBER => await _profileRepository.GetProfileByPhoneNumberAsync(loginRequestDto.UserName),
                _ => null
            };

            if (profileFromDb == null)
                throw new UnauthorizedException("Invalid credentials.");

            if (!BCrypt.Net.BCrypt.Verify(loginRequestDto.Password, profileFromDb.PasswordHash))
                throw new UnauthorizedException("Password mismatch.");

            string jwtToken = await GenerateJwtToken(profileFromDb);

            RefreshToken refreshToken = GenerateRefreshToken(profileFromDb.Id);

            string hashedRefreshToken = BCrypt.Net.BCrypt.HashPassword(refreshToken.Token);

            RefreshToken existingToken = await _refreshTokenRepository.GetByProfileIdAsync(profileFromDb.Id);

            if (existingToken != null)
            {
                existingToken.Token = hashedRefreshToken;
                existingToken.ExpiresAt = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:RefreshTokenExpiryDays"] ?? "37"));
                existingToken.CreatedAt = DateTime.UtcNow;
                existingToken.IsRevoked = false;
                await _refreshTokenRepository.UpdateAsync(existingToken);
            }
            else
            {
                refreshToken.Token = hashedRefreshToken;
                await _refreshTokenRepository.SaveAsync(refreshToken);
            }

            CookieOptions cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(37)
            };

            _httpContextAccessor.HttpContext!.Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

            return jwtToken;
        }

        private async Task<string> GenerateJwtToken(Profile profile)
        {

            SigningKey signingKey = await _signingKeyRepository.Query()
                .FirstOrDefaultAsync(k => k.IsActive) ?? throw new Exception("No active signing key available.");

            byte[] privateKeyBytes = Convert.FromBase64String(signingKey.PrivateKey);


            RSA rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(privateKeyBytes, out _);
            RsaSecurityKey rsaSecurityKey = new RsaSecurityKey(rsa)
            {
                KeyId = signingKey.KeyId
            };

            SigningCredentials creds = new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256);

            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,profile.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, profile.FirstName),
                new Claim(ClaimTypes.NameIdentifier, profile.Email),
                new Claim(ClaimTypes.Email, profile.Email),
                new Claim(ClaimTypes.PrimarySid, profile.Id.ToString())
            };

            foreach (var role in profile.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            JwtSecurityToken tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: "hms.com",
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpirationTime"])),
                signingCredentials: creds

             );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        private RefreshToken GenerateRefreshToken(Guid profileId)
        {
            string generatedRefreshToken = _refreshTokenRepository.GenerateRefreshToken();

            RefreshToken refreshToken = new RefreshToken
            {
                Token = generatedRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:RefreshTokenExpiryDays"] ?? "30")),
                CreatedAt = DateTime.UtcNow,
                ProfileId = profileId,
            };

            return refreshToken;
        }
        private bool IsRefreshTokenNearExpiry(RefreshToken token, int daysBeforeExpiry = 7)
        {
            if (token == null) return true;
            TimeSpan remainingTime = token.ExpiresAt - DateTime.UtcNow;
            return remainingTime.TotalDays <= daysBeforeExpiry;
        }


        public async Task<string> RefreshTokenAsync()
        {
            string? refreshTokenFromCookie = _httpContextAccessor.HttpContext?.Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshTokenFromCookie))

                throw new UnauthorizedException("Refresh token is missing.");

            RefreshToken refreshTokenFromDb = await _refreshTokenRepository.GetByTokenAsync(refreshTokenFromCookie);

            if (refreshTokenFromCookie == null || string.IsNullOrWhiteSpace(refreshTokenFromCookie) || refreshTokenFromDb == null)
            {
                throw new UnauthorizedException("RefreshToken is required");
            }
            if (refreshTokenFromDb.IsRevoked || refreshTokenFromDb.ExpiresAt < DateTime.UtcNow)
            {
                throw new UnauthorizedException("Token is expired or revoked");
            }

            Profile profileFromDb = await _profileService.GetByProfileId(refreshTokenFromDb.ProfileId);

            if (IsRefreshTokenNearExpiry(refreshTokenFromDb, 7))
            {
                refreshTokenFromDb.IsRevoked = true;
                await _refreshTokenRepository.UpdateAsync(refreshTokenFromDb);

                _httpContextAccessor.HttpContext?.Response.Cookies.Delete("refreshToken");

                throw new UnauthorizedAccessException("Refresh token is near expiry. Please login again.");
            }

            return await GenerateJwtToken(profileFromDb);

        }

        public async Task<bool> LogoutAsync()
        {
            string? refreshTokenInHttpCookie = _httpContextAccessor.HttpContext?.Request.Cookies["refreshToken"];

            bool flag = false;

            if (!string.IsNullOrEmpty(refreshTokenInHttpCookie))
            {
                RefreshToken tokenFromDb = await _refreshTokenRepository.GetByTokenAsync(refreshTokenInHttpCookie);

                bool tokenIsPresent = BCrypt.Net.BCrypt.Verify(refreshTokenInHttpCookie, tokenFromDb.Token);

                if (tokenIsPresent)
                {
                    tokenFromDb.IsRevoked = true;
                    tokenFromDb.RevokedAt = DateTime.UtcNow;
                    int res = await _refreshTokenRepository.UpdateAsync(tokenFromDb);
                    if (res > 0)
                    {
                        flag = true;
                    }
                }
            }

            CookieOptions cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(-1)
            };

            _httpContextAccessor.HttpContext!.Response.Cookies.Append("refreshToken", "", cookieOptions);

            return flag;
        }
    }
}
