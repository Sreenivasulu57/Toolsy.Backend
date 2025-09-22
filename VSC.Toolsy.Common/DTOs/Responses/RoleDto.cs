using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSC.Toolsy.Common.DTOs.Responses
{
    public class RoleDto
    {
        public string RoleName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
