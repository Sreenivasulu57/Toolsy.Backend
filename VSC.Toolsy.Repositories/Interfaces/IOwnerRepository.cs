using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Repositories.Interfaces
{
    public interface IOwnerRepository
    {
        int Save(Owner owner);
        Task<int> SaveAsync(Owner owner);
        List<Owner> GetAll();
        Task<List<Owner>> GetAllAsync();
        Task<int> UpdateAsync(Owner owner);
        Task<int> DeleteAsync(Owner owner);
        Task<Owner> GetByIdWithProfileAsync(Guid ownerId);
        Task<Owner> GetByOwnerId(Guid ownerId);
        Task<Owner> GetByProfileId(Guid id);
    }
}

