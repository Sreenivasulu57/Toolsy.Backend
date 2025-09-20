using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Common.Interfaces
{
    public interface IRoleService
    {
        Task<Role> DeleteRoleById(int id);
        Task<List<Role>> GetAllRolesAsync();
        Task<Role> GetByIdAsync(int id);
        Task<Role> SaveRoleAsync(RoleRequestDTO roleRequestDTO);
        Task<Role> UpdateRole(RoleUpdateRequestDTO roleUpdateRequestDTO, int id);
    }
}
