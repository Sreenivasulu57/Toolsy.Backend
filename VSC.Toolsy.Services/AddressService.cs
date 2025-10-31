using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.Exceptions;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.Interfaces;

namespace VSC.Toolsy.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IProfileService _profileService;
        private readonly IProfileRepository _profileRepository;
        public AddressService(IAddressRepository addressRepository, IProfileService profileService, IProfileRepository profileRepository)
        {
            _addressRepository = addressRepository;
            _profileService = profileService;
            _profileRepository = profileRepository;
        }

        public async Task<Address> UpdateAddress(AddressRegisterDto addressRegisterDto,string profileId)
        {
            if (!Guid.TryParse(profileId, out Guid profileGuid))
            {
                throw new UserNotFoundException($"user with id {profileId} is not present");
            }


          Profile  profileFromDb = await _profileService.GetProfileWithAddressByProfileId(profileGuid);

            if (profileFromDb == null || profileFromDb.IsDeleted) throw new AddressNotFoundException("Addresses not found");

            if (profileFromDb.Address != null &&
       profileFromDb.Address.ProfileId == profileGuid)
            {
                profileFromDb.Address.AddressLine1 = addressRegisterDto.AddressLine1;
                profileFromDb.Address.AddressLine2 = addressRegisterDto.AddressLine2;
                profileFromDb.Address.City = addressRegisterDto.City;
                profileFromDb.Address.Area = addressRegisterDto.Area;
                profileFromDb.Address.Mandal = addressRegisterDto.Mandal;
                profileFromDb.Address.District = addressRegisterDto.District;
                profileFromDb.Address.State = addressRegisterDto.State;
                profileFromDb.Address.PostalCode = addressRegisterDto.PostalCode;
                profileFromDb.Address.Country = addressRegisterDto.Country;
                profileFromDb.UpdatedBy = profileFromDb.Roles.ToString();
                profileFromDb.UpdatedAt = DateTime.UtcNow;

                int result = await _profileRepository.UpdateAsync(profileFromDb);

                if (result <= 0)
                {
                    throw new Exception("Internal Server Error");
                }
            }

            return profileFromDb.Address;

        }

        public async Task<List<Address>> GetAllAddresses()
        {
            List<Address> addresses = await _addressRepository.GetAllAsync();
            if (addresses.Count == 0) throw new AddressNotFoundException("Address not found");
            return addresses;
        }

        public async Task<Address> GetByProfileId(Guid profileId)
        {
            Profile profileFromDb = await _profileService.GetProfileWithAddressByProfileId(profileId);

            if (profileFromDb == null || !profileFromDb.Address.ProfileId.Equals(profileId)) return null;

            return profileFromDb.Address;

        }

        public async Task<Address> SaveAddressAsync(AddressRegisterDto addressRegisterDto,string profileId)
        {
            if (!Guid.TryParse(profileId, out Guid profileGuid))
            {
                throw new UserNotFoundException($"user with id {profileGuid} is not present");
            } 

            Profile profile = await _profileService.GetByProfileId(profileGuid);

            Address address = new Address()
            {
                AddressLine1 = addressRegisterDto.AddressLine1,
                AddressLine2 = addressRegisterDto.AddressLine2,
                City = addressRegisterDto.City,
                Area = addressRegisterDto.Area,
                Mandal = addressRegisterDto.Mandal,
                District = addressRegisterDto.District,
                State = addressRegisterDto.State,
                PostalCode = addressRegisterDto.PostalCode,
                Country = addressRegisterDto.Country,
                ProfileId = profile.Id
            };

            int result = await _addressRepository.SaveAsync(address);

            if (result <= 0)
            {
                throw new Exception("Internal Server Error");
            }

            return address;
        }

        public async Task<Address> GetAddressByProfileId(string profileId)
        {
            if(!Guid.TryParse(profileId,out Guid profileGuid))
            {
                throw new AddressNotFoundException($"Address not found for this id {profileId}");
            }

            return await _addressRepository.GetByProfileId(profileGuid);
        }
    }
}
