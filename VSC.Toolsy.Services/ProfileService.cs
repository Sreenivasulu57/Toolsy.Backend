using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.Enums;
using VSC.Toolsy.Common.Exceptions;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.Interfaces;

namespace VSC.Toolsy.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly ISigningKeyRepository _signingKeyRepository;
        private readonly IConfiguration _configuration;
        public ProfileService(IProfileRepository profileRepository, ISigningKeyRepository signingKeyRepository, IConfiguration configuration)
        {
            _profileRepository = profileRepository;
            _signingKeyRepository = signingKeyRepository;
            _configuration = configuration;
        }

        // This is for the Users only
        public async Task<Profile> GetByProfileId(Guid profileId)
        {
            Profile profile = await _profileRepository.GetByProfileId(profileId);

            return profile;
        }

        public async Task<Profile> GetProfileWithAddressByEmailAsync(string profileEmail)
            => await _profileRepository.GetProfileWithAddressByEmailAsync(profileEmail);

        public async Task<string> ProfileLoginAsync(LoginRequestDto loginRequestDto)
        {
            Profile profileFromDb = null!;
            if (loginRequestDto.Type.Equals(LoginType.EMAIL))
            {

                profileFromDb = await GetProfileByEmailAsync(loginRequestDto.UserName);
            }
            else if (loginRequestDto.Type.Equals(LoginType.NUMBER))
            {
                profileFromDb = await GetProfileByPhoneNumberAsync(loginRequestDto.UserName);

            }

            if (profileFromDb.Equals(null))
            {
                throw new UnauthorizedException("Invalid Credentials");
            }

            if (!profileFromDb.Role.Equals(loginRequestDto.Role))
            {
                throw new UnauthorizedAccessException($"You don't have an account with the role {loginRequestDto.Role}.");
            }

            if (!BCrypt.Net.BCrypt.Verify(loginRequestDto.Password, profileFromDb.PasswordHash))
            {
                throw new UnauthorizedException($"Password Is MisMatched");
            }

            return await GenerateJwtToken(profileFromDb);
        }

        public async Task<Profile> GetProfileByEmailAsync(string email)
            => await _profileRepository.GetByEmailAsync(email);


        public async Task<Profile> GetProfileByPhoneNumberAsync(string phoneNumber)
        {
            return await _profileRepository.GetProfileByPhoneNumberAsync(phoneNumber);
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
                new Claim(ClaimTypes.Role,profile.Role.ToString()),
            };

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
        public async Task<Profile> GetProfileWithAddressByProfileId(Guid profileId)
            => await _profileRepository.GetProfileWithAddressByProfileId(profileId);

    }
}
