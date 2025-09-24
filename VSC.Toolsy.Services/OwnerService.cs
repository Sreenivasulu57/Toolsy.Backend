using VSC.Toolsy.Common.DTOs.Requests;
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

        public OwnerService(IOwnerRepository ownerRepository, IProfileRepository profileRepository)
        {

            _ownerRepository = ownerRepository;
            _profileRepository = profileRepository;

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
