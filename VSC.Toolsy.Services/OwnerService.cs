using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.Configuration;
using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Enums;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.Interfaces;

namespace VSC.Toolsy.Services
{
    public class OwnerService : IOwnerService
    {

        private readonly IOwnerRepository _ownerRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IProfileService _profileService;
        private readonly IEmailService _emailService;
        private readonly string _defaultImage;
        public OwnerService(IOwnerRepository ownerRepository, IProfileRepository profileRepository, IProfileService profileService, IEmailService emailService, IConfiguration config)
        {
            _ownerRepository = ownerRepository;
            _profileRepository = profileRepository;
            _profileService = profileService;
            _emailService = emailService;
            _defaultImage = config["UserSettings:DefaultProfileImage"];
        }

        public async Task<OwnerResponseDto> GetOwnerByEmailAsync(string email)
        {
            Profile profileFromDb = await _profileService.GetProfileByEmailAsync(email);

            Owner ownerFromDb = await _ownerRepository.GetByProfileId(profileFromDb.Id);

            return MapToOwnerResponseDto(profileFromDb, ownerFromDb);
        }

        public async Task<OwnerResponseDto> GetOwnerByProfileIdAsync(Guid profileId)
        {
            Profile profileFromDb = await _profileService.GetProfileWithAddressByProfileId(profileId);

            Owner ownerFromDb = await _ownerRepository.GetByProfileId(profileFromDb.Id);

            return MapToOwnerResponseDto(profileFromDb, ownerFromDb);
        }

        public OwnerResponseDto MapToOwnerResponseDto(Profile profile, Owner owner)
        {
            return new OwnerResponseDto
            {
                ProfileId = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Email = profile.Email,
                PhoneNumber = profile.PhoneNumber,
                DateOfBirth = profile.DateOfBirth,
                Gender = profile.Gender,
                ProfileImageUrl = _defaultImage,
                IsActive = profile.IsActive,
                Status = profile.Status,
                VerificationStatus = profile.VerificationStatus,
                EmailVerifiedAt = profile.EmailVerifiedAt,
                PhoneVerifiedAt = profile.PhoneVerifiedAt,
                Roles = profile.Roles,
                Address = profile.Address,

                OwnerId = owner.Id,
                BusinessName = owner.BusinessName,
                BusinessDescription = owner.BusinessDescription,
                BusinessRegistrationNumber = owner.BusinessRegistrationNumber
            };
        }

        public async Task<Owner> GetOwnerWithProfileByOwerId(Guid ownerId)
        {
            Owner ownerFromDb = await _ownerRepository.GetByIdWithProfileAsync(ownerId);

            return ownerFromDb;
        }

        public async Task<Owner> RegisterOwner(OwnerRequestDto ownerRequestDto)
        {

            Profile ownerProfile = new Profile
            {

                FirstName = ownerRequestDto.FirstName,
                LastName = ownerRequestDto.LastName,
                Email = ownerRequestDto.Email,
                PhoneNumber = ownerRequestDto.PhoneNumber,
                DateOfBirth = ownerRequestDto.DateOfBirth,
                Gender = ownerRequestDto.Gender,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(ownerRequestDto.Password),
                ProfileImageUrl = _defaultImage,
                Roles = new List<UserRole> { UserRole.Owner },

                CreatedBy = UserRole.Owner.ToString()
            };

            int result = await _profileRepository.SaveAsync(ownerProfile);

            if (result <= 0)
            {
                throw new Exception("Internal Server Error");
            }

            Owner owner = new Owner
            {
                ProfileId = ownerProfile.Id,
                BusinessName = ownerRequestDto.BusinessName,
                BusinessDescription = ownerRequestDto.BusinessDescription,
                BusinessRegistrationNumber = ownerRequestDto.BusinessRegistrationNumber
            };

            int spResult = await _ownerRepository.SaveAsync(owner);

            if (spResult > 0)
            {
                _emailService.SendRegistrationSuccessEmailAsync(ownerProfile);
                return owner;
            }

            throw new Exception("Internal Server Error");
        }

        public async Task<OwnerResponseDto> GetOwnerByOwnerId(Guid ownerId)
        {
            Owner ownerFromDb = await _ownerRepository.GetByIdWithProfileAsync(ownerId);

            return MapToOwnerResponseDto(ownerFromDb.Profile, ownerFromDb);
        }
    }
}
