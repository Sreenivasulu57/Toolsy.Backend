using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        int Save(Address address);
        Task<int> SaveAsync(Address address);

        List<Address> GetAll();
        Task<List<Address>> GetAllAsync();

        Task<int> UpdateAsync(Address address);
        Task<int> DeleteAsync(Address address);
    }
}
