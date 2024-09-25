using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.Exceptions
{
    public class PerformerNotAvailableException : Exception
    {
        public PerformerNotAvailableException()
        {
        }

        public PerformerNotAvailableException(string message) : base(message)
        {
        }
    }
}
