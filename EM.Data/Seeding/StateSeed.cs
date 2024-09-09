using EM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Seeding
{
    public static class StateSeed
    {
        public  static List<State> GetStates()
        {
                return new List<State> {
                        new State { Id = 1, Name = "Andhra Pradesh", CountryId = 1 },
                        new State { Id = 2, Name = "Assam", CountryId = 1 },
                        new State { Id = 3, Name = "Arunachal Pradesh", CountryId = 1 },
                        new State { Id = 4, Name = "Bihar", CountryId = 1 },
                        new State { Id = 5, Name = "Gujrat", CountryId = 1 },
                        new State { Id = 6, Name = "Haryana", CountryId = 1 },
                        new State { Id = 7, Name = "Himachal Pradesh", CountryId = 1 },
                        new State { Id = 8, Name = "Jammu & Kashmir", CountryId = 1 },
                        new State { Id = 9, Name = "Karnataka", CountryId = 1 },
                        new State { Id = 10, Name = "Kerala", CountryId = 1 },
                        new State { Id = 11, Name = "Madhya Pradesh", CountryId = 1 },
                        new State { Id = 12, Name = "Maharashtra", CountryId = 1 },
                        new State { Id = 13, Name = "Manipur", CountryId = 1 },
                        new State { Id = 14, Name = "Meghalaya", CountryId = 1 },
                        new State { Id = 15, Name = "Mizoram", CountryId = 1 },
                        new State { Id = 16, Name = "Nagaland", CountryId = 1 },
                        new State { Id = 17, Name = "Orissa", CountryId = 1 },
                        new State { Id = 18, Name = "Punjab", CountryId = 1 },
                        new State { Id = 19, Name = "Rajasthan", CountryId = 1 },
                        new State { Id = 20, Name = "Sikkim", CountryId = 1 },
                        new State { Id = 21, Name = "Tamil Nadu", CountryId = 1 },
                        new State { Id = 22, Name = "Tripura", CountryId = 1 },
                        new State { Id = 23, Name = "Uttar Pradesh", CountryId = 1 },
                        new State { Id = 24, Name = "West Bengal", CountryId = 1 },
                        new State { Id = 25, Name = "Delhi", CountryId = 1 },
                        new State { Id = 26, Name = "Goa", CountryId = 1 },
                        new State { Id = 27, Name = "Pondichery", CountryId = 1 },
                        new State { Id = 28, Name = "Lakshadweep", CountryId = 1 },
                        new State { Id = 29, Name = "Daman & Diu", CountryId = 1 },
                        new State { Id = 30, Name = "Dadra & Nagar Haveli", CountryId = 1 },
                        new State { Id = 31, Name = "Chandigarh", CountryId = 1 },
                        new State { Id = 32, Name = "Andaman & Nicobar", CountryId = 1 },
                        new State { Id = 33, Name = "Uttarakhand", CountryId = 1 },
                        new State { Id = 34, Name = "Jharkhand", CountryId = 1 },
                        new State { Id = 35, Name = "Chhattisgarh", CountryId = 1 }
                };
        }
    }
}
