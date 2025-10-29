using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Common.Models.Pagination;

namespace VSC.Toolsy.Common.Interfaces
{
    public interface IToolCategoryService
    {
        #region Parent ToolCategory
        Task<bool> SaveToolCategoryAsync(ParentToolCategoryRequestDto parentToolCategoryRequestDto);

        Task<bool> UpdateToolCategoryAsync(Guid toolCategoryId,UpdateParentToolCategoryRequestDto updateParentToolCategoryRequestDto);

        Task<ToolCategory> GetByToolCategoryId(Guid toolCategoryId);

        Task<List<ToolCategoryResponseDto>> GetAllToolCategory();

        Task<bool> DeleteToolCategoryByIdAsync(Guid toolCategoryId);

        #endregion

        #region Sub ToolCategory
        Task<bool> SaveSubToolCategoryAsync(SubToolCategoryRequestDto SubToolCategoryRequestDto);

        Task<bool> UpdateSubToolCategoryAsync(Guid toolCategoryId,UpdateSubToolCategoryRequestDto updateParentToolCategoryRequestDto);

        Task<ToolCategory> GetBySubToolCategoryId(Guid toolCategoryId);

        Task<List<SubToolCategoryResponseDto>> GetAllSubToolCategory();

        Task<bool> DeleteSubToolCategoryByIdAsync(Guid toolCategoryId);

        Task<PaginatedResult<ToolResponseDto>> GetToolsBySubCategoryIdAsync(
        string subCategoryId,
        int page = 1,
        int pageSize = 10,
        string? sortBy = null,
        string? search = null,
        CancellationToken cancellationToken = default);
    }

    #endregion

}
