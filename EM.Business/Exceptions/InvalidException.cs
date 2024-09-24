using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.Exceptions
{
    public class InvalidException : Exception
    {

        public InvalidException() { }

        public InvalidException(string message) : base(message)
        {

        }
    }

}


