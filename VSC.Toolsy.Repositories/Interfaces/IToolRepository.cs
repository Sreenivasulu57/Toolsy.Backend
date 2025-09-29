using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Repositories.Interfaces
{
    public interface IToolRepository : IRepository<Tool>
    {
        Task<List<Tool>> GetAllByOwnerId(Guid ownerId);
        Task<List<Tool>> GetAllWithImagesAsync();
        Task<Tool> GetByToolId(Guid toolId);
    }
}
