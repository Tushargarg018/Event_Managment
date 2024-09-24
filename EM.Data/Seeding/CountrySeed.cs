using EM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Seeding
{
    public static class CountrySeed
    {
        public static List<Country> GetCountries()
        {
            return new List<Country>
            {
                new Country{Id = 1, Name="India"}
            };
        }
    }
}
