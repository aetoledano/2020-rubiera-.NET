using System;

namespace rubiera.Exceptions
{
    public class ExternalServiceFailedException : Exception
    {
        public ExternalServiceFailedException(string message) : base(message)
        {
        }
    }
}