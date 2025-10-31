using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Repositories.Interfaces
{
    public interface IToolRepository
    {
        int Save(Tool tool);
        Task<int> SaveAsync(Tool tool);
        List<Tool> GetAll();
        Task<List<Tool>> GetAllAsync();
        Task<int> UpdateAsync(Tool tool);
        Task<int> DeleteAsync(Tool tool);
        Task<List<Tool>> GetAllByOwnerId(Guid ownerId);
        Task<List<Tool>> GetAllWithImagesAsync();
        Task<ToolResponseDto> GetByFullToolId(Guid toolId);
        Task<Tool> GetByToolId(Guid toolId);
        Task<List<ToolResponseDto>> SearchTools(string query);

    }
}
