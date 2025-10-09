using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSC.Toolsy.Common.Exceptions
{
    public class ToolNotFoundException : Exception
    {
        public ToolNotFoundException(string message) : base(message)
        {

        }
    }
}
