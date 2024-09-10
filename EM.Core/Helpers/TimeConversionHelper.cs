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
            DateTime istDateTime = DateTime.ParseExact(date, "yyyy-MM-ddTHH:mm", null);
            TimeZoneInfo istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime utcDateTime = TimeZoneInfo.ConvertTimeToUtc(istDateTime, istTimeZone);
            return utcDateTime;
        }

        public static DateTime ConvertTimeFromUTC(DateTime date)
        {
            TimeZoneInfo istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime istDateTime = TimeZoneInfo.ConvertTimeFromUtc(date, istTimeZone);
            //string formattedDateTimeString = istDateTime.ToString("dd-MM-yyyyTHH:mm", CultureInfo.InvariantCulture);
            TruncateSeconds(istDateTime);
            return istDateTime;
        }

        public static DateTime TruncateSeconds(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0);
        }
    }
}
