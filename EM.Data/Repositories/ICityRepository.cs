using EM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Repositories
{
    public interface ICityRepository
    {
        public Task<IEnumerable<City>> GetCityList(int StateId);
        public Task<bool> StateExist(int StateId);
    }
}
