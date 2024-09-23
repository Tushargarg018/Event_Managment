using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.Exceptions
{
    public class EventAlreadyPublishedException: Exception
    {
        public EventAlreadyPublishedException()
        {
        }

        public EventAlreadyPublishedException(string message) : base(message)
        {
        }
    }
}
