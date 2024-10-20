using System.Runtime.InteropServices;

namespace notlcd
{
    public static class DateTimeService
    {
        public static DateTime GetCurrentDateTime()
        {
            string timeZoneId = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "Central European Standard Time" : "Europe/Warsaw";
            var dateTimeUtc = DateTime.UtcNow;
            var targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            var targetTime = TimeZoneInfo.ConvertTimeFromUtc(dateTimeUtc, targetTimeZone);

            return targetTime;
        }
    }
}