using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Helpers
{
    public static class TimeHelper
    {
        private static readonly TimeZoneInfo vietnameTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        public static DateTime GetTimeNow()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnameTimeZone);
        }
    }
}
