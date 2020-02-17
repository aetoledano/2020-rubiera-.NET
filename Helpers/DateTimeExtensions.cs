using System;

namespace rubiera.Helpers
{
    public static class DateTimeExtensions
    {
        private static DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long currentTimeMillis()
        {
            TimeSpan javaSpan = DateTime.UtcNow - Jan1st1970;
            return (long) javaSpan.TotalMilliseconds;
        }
    }
}