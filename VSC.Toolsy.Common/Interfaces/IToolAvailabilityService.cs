using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Common.Interfaces
{
    public interface IToolAvailabilityService
    {
        Task<bool> SaveToolAvailabilityAsync(ToolAvailabilityRequestDto toolAvailabilityRequestDto);

        Task<bool> UpdateToolAvailabilityAsync(Guid toolAvailabilityId,ToolAvailabilityRequestDto toolAvailabilityRequestDto);

        Task<ToolAvailability> GetByToolAvailabilityIdAsync(Guid toolAvailabilityId);

        Task<List<ToolAvailability>> GetAllToolAvailabilityAsync();

        Task<bool> DeleteByToolAvailabilityId(Guid toolAvailabilityId);
    }
}
