using Microsoft.EntityFrameworkCore;
using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Enums;
using VSC.Toolsy.Common.Exceptions;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.Interfaces;

namespace VSC.Toolsy.Services
{
    public class ToolCategoryService : IToolCategoryService
    {
        private readonly IToolCategoryRepository _toolCategoryRepositoiry;
        public ToolCategoryService(IToolCategoryRepository toolCategoryRepositoiry)
        {
            _toolCategoryRepositoiry = toolCategoryRepositoiry;
        }

        #region Parent ToolCategory

        public async Task<bool> SaveToolCategoryAsync(ParentToolCategoryRequestDto parentToolCategoryRequestDto)
        {
            ToolCategory toolCategory = new ToolCategory
            {
                Name = parentToolCategoryRequestDto.Name,
                Description = parentToolCategoryRequestDto.Description,
                IconUrl = parentToolCategoryRequestDto.IconUrl,
                SubCategories = new List<ToolCategory>(),
                IsActive = true,
                CreatedBy = UserRole.Admin.ToString()
            };

            int result = await _toolCategoryRepositoiry.SaveAsync(toolCategory);

            if (result <= 0)
                throw new Exception("Internal server error");
            else
                return true;
        }

        public async Task<bool> DeleteToolCategoryByIdAsync(Guid toolCategoryId)
        {
            ToolCategory toolCategory = await _toolCategoryRepositoiry.GetByIdAsync(toolCategoryId);

            toolCategory.DeletedAt = DateTime.UtcNow;
            toolCategory.DeletedBy = UserRole.Admin.ToString();
            toolCategory.IsActive = false;
            toolCategory.IsDeleted = false;

            int result = await _toolCategoryRepositoiry.UpdateAsync(toolCategory);

            if (result <= 0)
                throw new Exception("Internal server error");
            else
                return true;
        }

        public async Task<List<ToolCategoryResponseDto>> GetAllToolCategory()
            => await _toolCategoryRepositoiry.GetAllToolCategoryAsync();

        public async Task<ToolCategory> GetByToolCategoryId(Guid toolCategoryId)
            => await _toolCategoryRepositoiry.GetByIdAsync(toolCategoryId);

        public async Task<bool> UpdateToolCategoryAsync(Guid toolCategoryId, UpdateParentToolCategoryRequestDto updateParentToolCategoryRequestDto)
        {
            ToolCategory toolCategory = await _toolCategoryRepositoiry.GetByIdAsync(toolCategoryId);

            toolCategory.Name = updateParentToolCategoryRequestDto.Name;
            toolCategory.Description = updateParentToolCategoryRequestDto.Description;
            toolCategory.IconUrl = updateParentToolCategoryRequestDto.IconUrl;
            toolCategory.UpdatedAt = DateTime.UtcNow;
            toolCategory.UpdatedBy = UserRole.Admin.ToString();

           int result = await _toolCategoryRepositoiry.UpdateAsync(toolCategory);

            if (result <= 0)
                throw new Exception("Internal server error");
            else
                return true;

        }

        #endregion



        #region Sub ToolCategory

        public async Task<bool> SaveSubToolCategoryAsync(SubToolCategoryRequestDto subToolCategoryRequestDto)
        {
            ToolCategory subToolCategory = new ToolCategory
            {
                Name = subToolCategoryRequestDto.Name,
                Description = subToolCategoryRequestDto.Description,
                IconUrl = subToolCategoryRequestDto.IconUrl,
                ParentCategoryId = subToolCategoryRequestDto.ParentCategoryId,
                CreatedBy = UserRole.Admin.ToString(),
            };
            
            ToolCategory toolCategoryFromDb = await _toolCategoryRepositoiry.GetToolCategoryByParentId(subToolCategoryRequestDto.ParentCategoryId);

            int result = await _toolCategoryRepositoiry.SaveAsync(subToolCategory);

            if (result <= 0)
                throw new Exception("Internal server error");
            else
                return true;

        }

        public async Task<List<SubToolCategoryResponseDto>> GetAllSubToolCategory()
          => await _toolCategoryRepositoiry.GetAllSubToolCategoryAsync();

        public async Task<bool> DeleteSubToolCategoryByIdAsync(Guid subToolCategoryId)
        {
            ToolCategory subToolCategory = await _toolCategoryRepositoiry.GetByIdAsync(subToolCategoryId);

            subToolCategory.IsDeleted = true;
            subToolCategory.DeletedBy = UserRole.Admin.ToString();
            subToolCategory.DeletedAt = DateTime.UtcNow;
            subToolCategory.Tools = null;


            int result = await _toolCategoryRepositoiry.UpdateAsync(subToolCategory);

            if (result <= 0)
                throw new Exception("Internal server error");
            else
                return true;
        }

        public async Task<bool> UpdateSubToolCategoryAsync(Guid subToolCategoryId,UpdateSubToolCategoryRequestDto updateSubToolCategoryRequestDto)
        {
            ToolCategory subToolCategory = await _toolCategoryRepositoiry.GetByIdAsync(subToolCategoryId);
            subToolCategory.Name = updateSubToolCategoryRequestDto.Name;
            subToolCategory.Description = updateSubToolCategoryRequestDto.Description;
            subToolCategory.IconUrl = updateSubToolCategoryRequestDto.IconUrl;
            subToolCategory.ParentCategoryId = updateSubToolCategoryRequestDto.ParentCategoryId;
            subToolCategory.UpdatedBy = UserRole.Admin.ToString();
            subToolCategory.UpdatedAt = DateTime.UtcNow;

            int result = await  _toolCategoryRepositoiry.UpdateAsync(subToolCategory);

            if (result <= 0)
                throw new Exception("Internal server error");
            else
                return true;
        }

        public async Task<ToolCategory> GetBySubToolCategoryId(Guid subToolCategoryId)
             => await _toolCategoryRepositoiry.GetByIdAsync(subToolCategoryId);

        #endregion
    }
}
