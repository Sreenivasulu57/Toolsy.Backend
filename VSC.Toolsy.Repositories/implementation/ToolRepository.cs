using Microsoft.EntityFrameworkCore;
using VSC.Toolsy.Common.Exceptions;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.Data;
using VSC.Toolsy.Repositories.Interfaces;

namespace VSC.Toolsy.Repositories.implementation
{
    public class ToolRepository : Repository<Tool>, IToolRepository
    {
        private readonly IOwnerRepository _ownerRepository;
        public ToolRepository(ApplicationDbContext applicationDbContext, IOwnerRepository ownerRepository) : base(applicationDbContext)
        {
            _ownerRepository = ownerRepository;
        }

        public async Task<List<Tool>> GetAllByOwnerId(Guid ownerId)

            => await Query()
            .Where(t => t.OwnerId.Equals(ownerId)).Include(t => t.ToolImages)
            .ToListAsync() ?? throw new OwnerNotFoundException($"Owner not found for this id :{ownerId}");

        public async Task<List<Tool>> GetAllWithImagesAsync()
            => await Query()
            .Include(t => t.ToolImages)
            .ToListAsync();


        public async Task<Tool> GetByToolId(Guid toolId)

            => await Query().Where(t => t.Id.Equals(toolId)).
            FirstOrDefaultAsync() ?? throw new ToolNotFoundException($"Tool this Id {toolId} is not found");


    }
}
