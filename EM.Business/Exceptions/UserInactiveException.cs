using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.Exceptions
{
    public class UserInactiveException : Exception
    {
     
        public UserInactiveException() { }

        public UserInactiveException(string message) : base(message)
        {
      
        }
    }

}
