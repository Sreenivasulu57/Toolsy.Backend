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
        public OwnerService(IOwnerRepository ownerRepository, IProfileRepository profileRepository, IProfileService profileService)
        {

            _ownerRepository = ownerRepository;
            _profileRepository = profileRepository;
            _profileService = profileService;

        }



        public async Task<OwnerResponseDto> GetOwnerByEmailAsync(string email)
        {
            Profile profileFromDb = await _profileService.GetProfileWithAddressByEmailAsync(email);

            Owner ownerFromDb = await _ownerRepository.GetByProfileId(profileFromDb.Id);

            OwnerResponseDto ownerResponseDto = new OwnerResponseDto
            {
                ProfileId = profileFromDb.Id,
                FirstName = profileFromDb.FirstName,
                LastName = profileFromDb.LastName,
                Email = profileFromDb.Email,
                PhoneNumber = profileFromDb.PhoneNumber,
                DateOfBirth = profileFromDb.DateOfBirth,
                Gender = profileFromDb.Gender,
                ProfileImageUrl = profileFromDb.ProfileImageUrl,
                IsActive = profileFromDb.IsActive,
                Status = profileFromDb.Status,
                VerificationStatus = profileFromDb.VerificationStatus,
                EmailVerifiedAt = profileFromDb.EmailVerifiedAt,
                PhoneVerifiedAt = profileFromDb.PhoneVerifiedAt,
                Role = profileFromDb.Role,
                Address = profileFromDb.Address,

                OwnerId = ownerFromDb.Id,
                BusinessName = ownerFromDb.BusinessName,
                BusinessDescription = ownerFromDb.BusinessDescription,
                BusinessRegistrationNumber = ownerFromDb.BusinessRegistrationNumber


            };

            return ownerResponseDto;

        }
        public async Task<Owner> GetOwnerWithProfileByEmailAsync(string ownerEmail)
        {
            Profile ownerProfileFromDb = await _profileService.GetProfileByEmailAsync(ownerEmail);

            Owner ownerFromDb = await _ownerRepository.GetByProfileId(ownerProfileFromDb.Id);

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
                ProfileImageUrl = ownerRequestDto.ProfileImageUrl,
                Role = Role.Owner,

                CreatedBy = Role.Owner.ToString()

            };

            _profileRepository.Save(ownerProfile);
            int result = await _profileRepository.SaveChangesAsync();

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

            _ownerRepository.Save(owner);
            int spResult = await _ownerRepository.SaveChangesAsync();

            if (spResult > 0)
            {
                return owner;
            }

            throw new Exception("Internal Server Error");


        }
    }
}
