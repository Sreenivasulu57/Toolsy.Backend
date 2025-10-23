using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Common.Interfaces
{
    public interface IToolSpecificationService
    {
        Task<bool> SaveToolSpecificationAsync(ToolSpecificationRequestDto toolSpecificationRequestDto);

        Task<bool> UpdateToolSpecificationAsync(Guid toolSpecificationId, ToolSpecificationRequestDto toolSpecificationRequestDto);

        Task<ToolSpecification> GetToolSpecificationById(Guid toolSpecificationId);

        Task<List<ToolSpecification>> GetAllToolSpecification();

        Task<bool> DeleteToolSpecificationById(Guid toolSpecificationId);
    }
}
