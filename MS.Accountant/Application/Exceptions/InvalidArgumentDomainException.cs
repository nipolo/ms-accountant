using System;

namespace MS.Accountant.Application.Exceptions
{
    public class InvalidArgumentDomainException : Exception
    {
        public InvalidArgumentDomainException(string argumentName, object argumentValue)
            : base($"Invalid argument {argumentName}: {argumentValue}")
        {
            
        }
    }
}
