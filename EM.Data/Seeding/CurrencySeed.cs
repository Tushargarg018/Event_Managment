using EM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Seeding
{
    public static class CurrencySeed
    {
        public static List<Currency> GetCurrencies()
        {
            return new List<Currency>
            {
                new Currency{Id = 1, CountryId = 1, CurrencyCode = "INR", CountryCode="IN", Symbol="\u20B9"}
            };
        }
    }
}
