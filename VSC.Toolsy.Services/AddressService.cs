using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.Interfaces;

namespace VSC.Toolsy.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IProfileService _profileService;
        public AddressService(IAddressRepository addressRepository, IProfileService profileService)
        {
            _addressRepository = addressRepository;
            _profileService = profileService;
        }
        public async Task<Address> SaveAddressAsync(AddressRegisterDto addressRegisterDto)
        {
            Profile profile = await _profileService.GetProfileByEmailAsync(addressRegisterDto.ProfileEmail);

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

            await _addressRepository.SaveAsync(address);

            await _addressRepository.SaveChangesAsync();

            return address;
        }
    }
}
