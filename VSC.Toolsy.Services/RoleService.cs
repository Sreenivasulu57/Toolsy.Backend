using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.implementation;
using VSC.Toolsy.Repositories.Interfaces;

namespace VSC.Toolsy.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Role> DeleteRoleById(int id)
        {
            DateTime now = DateTime.UtcNow;

            Role roleFromDb = await GetByIdAsync(id);

            roleFromDb.IsDeleted = true;
            roleFromDb.IsActive = false;

            roleFromDb.DeletedAt = now;
            roleFromDb.UpdatedAt = now;

            _roleRepository.Update(roleFromDb);
            await _roleRepository.SaveChangesAsync();

            return roleFromDb;

        }

        public async Task<List<Role>> GetAllRolesAsync()
            => await _roleRepository.GetAllAsync();

        public async Task<Role> GetByIdAsync(int id)
            => await _roleRepository.GetByIdAsync(id);

        public async Task<Role> SaveRoleAsync(RoleRequestDTO roleRequestDTO)
        {
            Role role = new Role
            {
                Name = roleRequestDTO.Name,
                Description = roleRequestDTO.Description
            };

            await _roleRepository.SaveAsync(role);
            await _roleRepository.SaveChangesAsync();

            return role;
        }

        public async Task<Role> UpdateRole(RoleUpdateRequestDTO roleUpdateRequestDTO, int id)
        {

            Role roleFromDb = await GetByIdAsync(id);

            roleFromDb.Name = roleUpdateRequestDTO.Name;
            roleFromDb.Description = roleUpdateRequestDTO.Description;
            roleFromDb.IsActive = roleUpdateRequestDTO.IsActive;
            roleFromDb.UpdatedAt = DateTime.UtcNow;

            _roleRepository.Update(roleFromDb);
            int result = await _roleRepository.SaveChangesAsync();

            if (result <= 0)
            {
                throw new Exception("Internal Server Error While Updating The Role");
            }

            return roleFromDb;
        }
    }
}
