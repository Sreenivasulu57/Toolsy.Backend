using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Repositories.Interfaces
{
    public interface IToolAvailabilityRepository
    {
        int Save(ToolAvailability toolAvailability);

        Task<int> SaveAsync(ToolAvailability toolAvailability);

        int Update(ToolAvailability toolAvailability);

        Task<int> UpdateAsync(ToolAvailability toolAvailability);

        ToolAvailability GetById(Guid toolAvailabilityId);

        Task<ToolAvailability> GetByIdAsync(Guid toolAvailabilityId);

        List<ToolAvailability> GetAllToolAvailability();

        Task<List<ToolAvailability>> GetAllToolAvailabilityAsync();

        int Delete(ToolAvailability toolAvailability);

        Task<int> DeleteAsync(ToolAvailability toolAvailability);
    }
}
