using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Repositories.Interfaces
{
    public interface IToolCategoryRepository
    {
        int Save(ToolCategory toolCategory);

        Task<int> SaveAsync(ToolCategory toolCategory);

        int Update(ToolCategory toolCategory);

        Task<int> UpdateAsync(ToolCategory toolCategory);

        Task<ToolCategory> GetByIdAsync(Guid toolCategoryId);

        Task<List<ToolCategoryResponseDto>> GetAllToolCategoryAsync();

        Task<List<SubToolCategoryResponseDto>> GetAllSubToolCategoryAsync();

        int Delete(ToolCategory toolCategory);

        Task<int> DeleteAsync(ToolCategory toolCategory);

        Task<ToolCategory> GetToolCategoryByParentId(Guid parentCategoryId);
    }
}
