using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.Interfaces;

namespace VSC.Toolsy.Services
{
    public class ToolSpecificationService:IToolSpecificationService
    {
        private readonly IToolSpecificationRepository _toolSpecificationRepository;

        public ToolSpecificationService(IToolSpecificationRepository toolSpecificationRepository)
        {
            _toolSpecificationRepository = toolSpecificationRepository;
        }

        public async Task<bool> SaveToolSpecificationAsync(ToolSpecificationRequestDto toolSpecificationRequestDto)
        {
            ToolSpecification toolSpecification = new ToolSpecification
            {
                Name = toolSpecificationRequestDto.Name,
                Value = toolSpecificationRequestDto.Value,
                Unit = toolSpecificationRequestDto.Unit,
                ToolId = toolSpecificationRequestDto.ToolId,
            };

          int result = await  _toolSpecificationRepository.SaveAsync(toolSpecification);
            if (result <= 0)
                return false;
            else
                return true;
        }

        public async Task<bool> UpdateToolSpecificationAsync(Guid toolSpecificationId, ToolSpecificationRequestDto toolSpecificationRequestDto)
        {
            ToolSpecification toolSpecificationFromDb = await _toolSpecificationRepository.GetByIdAsync(toolSpecificationId);

            toolSpecificationFromDb.UpdatedAt = DateTime.UtcNow;
            toolSpecificationFromDb.Name = toolSpecificationRequestDto.Name;
            toolSpecificationFromDb.Value = toolSpecificationRequestDto.Value;
            toolSpecificationFromDb.Unit = toolSpecificationRequestDto.Unit;
            toolSpecificationFromDb.ToolId = toolSpecificationRequestDto.ToolId;

            int result = _toolSpecificationRepository.Update(toolSpecificationFromDb);

            if (result <= 0)
                return false;
            else
                return true;

        }

        public async Task<ToolSpecification> GetToolSpecificationById(Guid toolSpecificationId)
            => await _toolSpecificationRepository.GetByIdAsync(toolSpecificationId);

        public async Task<List<ToolSpecification>> GetAllToolSpecification()
            => await _toolSpecificationRepository.GetAllAsync();

        public async Task<bool> DeleteToolSpecificationById(Guid toolSpecificationId)
        {
            ToolSpecification toolSpecificationFromDb = await _toolSpecificationRepository.GetByIdAsync(toolSpecificationId);

            toolSpecificationFromDb.IsDeleted = true;
            toolSpecificationFromDb.DeletedAt = DateTime.UtcNow;

            int result = await _toolSpecificationRepository.UpdateAsync(toolSpecificationFromDb);

            if (result <= 0)
                return false;
            else
                return true;

        }

    }
}
