using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.Interfaces;

namespace VSC.Toolsy.Services
{
    public class ToolAvailabilityService : IToolAvailabilityService
    {
        private readonly IToolAvailabilityRepository _toolAvailabilityRepository;

        public ToolAvailabilityService(IToolAvailabilityRepository toolAvailabilityRepository)
        {
            _toolAvailabilityRepository = toolAvailabilityRepository;
        }


        public async  Task<bool> DeleteByToolAvailabilityId(Guid toolAvailabilityId)
        {
            ToolAvailability toolAvailabilityFromDb = await _toolAvailabilityRepository.GetByIdAsync(toolAvailabilityId);

            toolAvailabilityFromDb.IsDeleted = true;
            toolAvailabilityFromDb.DeletedAt = DateTime.UtcNow;
            toolAvailabilityFromDb.IsAvailable = false;

            int result = await _toolAvailabilityRepository.UpdateAsync(toolAvailabilityFromDb);

            if (result <= 0)
                return false;
            else
                return true;

        }

        public async Task<List<ToolAvailability>> GetAllToolAvailabilityAsync()
            =>await  _toolAvailabilityRepository.GetAllToolAvailabilityAsync();

        public async Task<ToolAvailability> GetByToolAvailabilityIdAsync(Guid toolAvailabilityId)
            => await _toolAvailabilityRepository.GetByIdAsync(toolAvailabilityId);

        public async Task<bool> SaveToolAvailabilityAsync(ToolAvailabilityRequestDto toolAvailabilityRequestDto)
        {
            ToolAvailability toolAvailability = new ToolAvailability
            {
                Date = toolAvailabilityRequestDto.Date,
                StartTime = toolAvailabilityRequestDto.StartTime,
                EndTime = toolAvailabilityRequestDto.EndTime,
                IsAvailable = true,
                Notes = toolAvailabilityRequestDto.Notes,
                ToolId = toolAvailabilityRequestDto.ToolId
            };

            int result = await _toolAvailabilityRepository.SaveAsync(toolAvailability);

            if (result <= 0)
                return false;
            else
                return true;
        }

        public async  Task<bool> UpdateToolAvailabilityAsync(Guid toolAvailabilityId,ToolAvailabilityRequestDto toolAvailabilityRequestDto)
        {
            ToolAvailability toolAvailabilityFromDb = await _toolAvailabilityRepository.GetByIdAsync(toolAvailabilityId);

            toolAvailabilityFromDb.UpdatedAt = DateTime.UtcNow;
            toolAvailabilityFromDb.Date = toolAvailabilityRequestDto.Date;
            toolAvailabilityFromDb.StartTime = toolAvailabilityRequestDto.StartTime;
            toolAvailabilityFromDb.EndTime = toolAvailabilityRequestDto.EndTime;
            toolAvailabilityFromDb.IsAvailable = toolAvailabilityRequestDto.IsAvailable;
            toolAvailabilityFromDb.Notes = toolAvailabilityRequestDto.Notes;
            toolAvailabilityFromDb.ToolId = toolAvailabilityRequestDto.ToolId;

            int result = await _toolAvailabilityRepository.UpdateAsync(toolAvailabilityFromDb);

            if (result <= 0)
                return false;
            else
                return true;

        }
    }
}
