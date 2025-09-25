
using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Common.Interfaces
{
    public interface IToolService
    {
        Task<Tool> SaveToolAsync(ToolRequestDto toolRequestDto);
    }
}
