using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSC.Toolsy.Common.Enums
{
    public enum Policy
    {
        USER_ONLY,
        OWNER_ONLY,
        ADMIN_ONLY,
        ADMIN_OR_OWNER,
        AUTHENTICATED_PROFILE
    }
}
