using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Common.Interfaces
{
    public interface IAddressService
    {
        public Task<Address> SaveAddressAsync(AddressRegisterDto addressRegisterDto);
        public Task<Address> UpdateAddress(AddressRegisterDto addressRegisterDto);
        public Task<Address> GetByProfileId(Guid profileId);
        public Task<List<Address>> GetAllAddresses();

    }
}
