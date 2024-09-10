using EM.Business.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.ServiceImpl
{
    public class TimeService : ITimeService
    {
        public DateTime ConvertISTToUTC(string date)
        {
            DateTime istDateTime = DateTime.ParseExact(date, "dd-MM-yyyyTHH:mm", null);

            TimeZoneInfo istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime utcDateTime = TimeZoneInfo.ConvertTimeToUtc(istDateTime, istTimeZone);

            return utcDateTime;
        }

        public DateTime ConvertTimeFromUTC(DateTime date)
        {
            TimeZoneInfo istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime istDateTime = TimeZoneInfo.ConvertTimeFromUtc(date, istTimeZone);
            //string formattedDateTimeString = istDateTime.ToString("dd-MM-yyyyTHH:mm", CultureInfo.InvariantCulture);
            return istDateTime;
        }
    }
}
