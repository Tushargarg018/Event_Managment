using EM.Business.BOs;
using EM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.Services
{
    public interface ICityService
    {
        public Task<IEnumerable<CityBo>> GetCities(int StateId);
    }
}
