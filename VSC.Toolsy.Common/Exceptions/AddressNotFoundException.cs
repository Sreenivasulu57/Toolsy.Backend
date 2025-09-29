using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSC.Toolsy.Common.Exceptions
{
    public class AddressNotFoundException : Exception
    {
        public AddressNotFoundException(string message) : base(message)
        {

        }
    }
}
