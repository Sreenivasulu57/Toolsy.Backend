using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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

        public AuthService(IProfileService profileService, IProfileRepository profileRepository,
            ISigningKeyRepository signingKeyRepository, IConfiguration configuration, IRefreshTokenRepository refreshTokenRepository)
        {
            _profileService = profileService;
            _profileRepository = profileRepository;
            _signingKeyRepository = signingKeyRepository;
            _configuration = configuration;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<TokenResponseDto> ProfileLoginAsync(LoginRequestDto loginRequestDto)
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
                existingToken.ExpiresAt = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:RefreshTokenExpiryDays"] ?? "30"));
                existingToken.CreatedAt = DateTime.UtcNow;
                existingToken.IsRevoked = false;
                await _refreshTokenRepository.UpdateAsync(existingToken);
            }
            else
            {
                await _refreshTokenRepository.SaveAsync(refreshToken);
            }

            return new TokenResponseDto
            {
                Token = jwtToken,
                RefreshToken = refreshToken.Token
            };
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

        public async Task<TokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDTO refreshTokenRequestDTO)
        {
            RefreshToken refreshToken = await _refreshTokenRepository.GetByProfileIdAsync(refreshTokenRequestDTO.ProfileId);

            if (refreshTokenRequestDTO == null || string.IsNullOrWhiteSpace(refreshTokenRequestDTO.RefreshToken) || refreshToken == null)
            {
                throw new UnauthorizedException("RefreshToken is required");
            }
            if (refreshToken.IsRevoked || refreshToken.ExpiresAt < DateTime.UtcNow)
            {
                throw new UnauthorizedException("Token is expired or revoked");
            }

            Profile profileFromDb = await _profileService.GetByProfileId(refreshTokenRequestDTO.ProfileId);

            string newJwtToken = await GenerateJwtToken(profileFromDb);

            return new TokenResponseDto
            {
                Token = newJwtToken,
                RefreshToken = refreshTokenRequestDTO.RefreshToken
            };
        }
    }
}
