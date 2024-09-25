using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.Exceptions
{
    public class VenueNotAvailableException : Exception
    {
        public VenueNotAvailableException()
        {
        }

        public VenueNotAvailableException(string message) : base(message)
        {
        }
    }
}
