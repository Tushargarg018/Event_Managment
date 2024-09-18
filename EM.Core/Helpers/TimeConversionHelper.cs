using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Core.Helpers
{
    public static class TimeConversionHelper
    {
        public static DateTime ConvertISTToUTC(string date)
        {
            DateTime istDateTime = DateTime.ParseExact(date, "yyyy-MM-ddTHH:mm:ss", null);
            TimeZoneInfo istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime utcDateTime = TimeZoneInfo.ConvertTimeToUtc(istDateTime, istTimeZone);
            return utcDateTime;
        }

        public static DateTime ConvertTimeFromUTC(DateTime date)
        {
            TimeZoneInfo istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime istDateTime = TimeZoneInfo.ConvertTimeFromUtc(date, istTimeZone);
            return istDateTime;
        }

        public static string ConvertISTtoUTC(DateTime date)
        {
            TimeZoneInfo istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime istDateTime = TimeZoneInfo.ConvertTimeFromUtc(date, istTimeZone);
            return istDateTime.ToString("yyyy-MM-ddTHH:mm:ss");
        }
        public static DateTime TruncateSeconds(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0);
        }
        public static string ConvertISOFormat(DateTime date)
        {
           return date.ToString("yyyy-MM-ddTHH:mm:ss");
        }
        public static string ToCustomDateTimeString(DateTime dateTime)
        {
            // Convert to local time and format without milliseconds or time zone
            return dateTime.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:ss");
        }
    }
}
