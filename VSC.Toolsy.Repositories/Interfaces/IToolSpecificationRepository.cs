using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Repositories.Interfaces
{
    public interface IToolSpecificationRepository
    {
        int Save(ToolSpecification toolSpecification);
        Task<int> SaveAsync(ToolSpecification toolSpecification);
        int Update(ToolSpecification toolSpecification);
        Task<int> UpdateAsync(ToolSpecification toolSpecification);
        int Delete(ToolSpecification toolSpecification);
        Task<int> DeleteAsync(ToolSpecification toolSpecification);
        ToolSpecification GetById(Guid toolSpecificationId);
        Task<ToolSpecification> GetByIdAsync(Guid toolSpecificationId);
        List<ToolSpecification> GetAll();
        Task<List<ToolSpecification>> GetAllAsync();

    }
}
