using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Common.Interfaces
{
    public interface IAddressService
    {
        public Task<Address> SaveAddressAsync(AddressRegisterDto addressRegisterDto,string profileId);
        public Task<Address> UpdateAddress(AddressRegisterDto addressRegisterDto,string profileId);
        public Task<Address> GetByProfileId(Guid profileId);
        public Task<List<Address>> GetAllAddresses();
        public Task<Address> GetAddressByProfileId(string profileId);


    }
}
