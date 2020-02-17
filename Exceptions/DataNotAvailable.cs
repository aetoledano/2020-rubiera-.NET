using System;

namespace rubiera.Exceptions
{
    public class DataNotAvailable : Exception
    {
        public DataNotAvailable(string message) : base(message)
        {
        }
    }
}