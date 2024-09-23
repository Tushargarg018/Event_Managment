using System;

namespace EM.Business.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName)
            : base($"{entityName} does not exist.") { }
    }
}
