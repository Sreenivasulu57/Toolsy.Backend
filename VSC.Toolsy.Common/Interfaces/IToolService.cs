using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Common.Interfaces
{
    public interface IToolService
    {
        Task<Tool> Save(ToolRequestDto toolRequestDto);
        Task<List<Tool>> GetAllTByOwnerId(Guid ownerProfileId);
        Task<Tool> UpdateByToolId(ToolRequestDto toolRequestDto, Guid toolId);
        Task<Tool> DeleteByToolId(Guid toolId);
        Task<List<Tool>> GetAll();
    }
}
