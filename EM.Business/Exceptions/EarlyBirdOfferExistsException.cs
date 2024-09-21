using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.Exceptions
{
    public class EarlyBirdOfferExistsException : Exception
    {
        public EarlyBirdOfferExistsException()
        {
        }

        public EarlyBirdOfferExistsException(string message) : base(message)
        {
        }
    }
}
