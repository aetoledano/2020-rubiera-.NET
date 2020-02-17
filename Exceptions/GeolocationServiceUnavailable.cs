using System;

namespace rubiera.Exceptions
{
    public class GeolocationServiceUnavailable : Exception
    {
        public GeolocationServiceUnavailable(string message) : base(message)
        {
        }
    }
}