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
        public ProfileService(IProfileRepository profileRepository, ISigningKeyRepository signingKeyRepository)
        {
            _profileRepository = profileRepository;
        }

        // This is for the Users only
        public async Task<Profile> GetByProfileId(Guid profileId)
            => await _profileRepository.GetByProfileId(profileId);

        public async Task<Profile> GetProfileWithAddressByEmailAsync(string profileEmail)
            => await _profileRepository.GetProfileWithAddressByEmailAsync(profileEmail);

        public async Task<Profile> GetProfileByEmailAsync(string email)
            => await _profileRepository.GetByEmailAsync(email);

        public async Task<Profile> GetProfileByPhoneNumberAsync(string phoneNumber)
            => await _profileRepository.GetProfileByPhoneNumberAsync(phoneNumber);

        public async Task<Profile> GetProfileWithAddressByProfileId(Guid profileId)
            => await _profileRepository.GetProfileWithAddressByProfileId(profileId);

    }
}
