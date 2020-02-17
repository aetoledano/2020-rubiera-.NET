using System;

namespace rubiera.Exceptions
{
    public class CityNotFoundException:Exception
    {
        public CityNotFoundException(string message) : base(message)
        {
        }
    }
}