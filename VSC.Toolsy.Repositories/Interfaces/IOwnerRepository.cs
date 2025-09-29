using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Repositories.Interfaces
{
    public interface IOwnerRepository : IRepository<Owner>
    {
        Task<Owner> GetByIdWithProfileAsync(Guid ownerId);
        Task<Owner> GetByOwnerId(Guid ownerId);
        Task<Owner> GetByProfileId(Guid id);
    }
}
