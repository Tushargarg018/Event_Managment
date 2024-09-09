using EM.Data;
using EM.Data.Entities;
using EM.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.Services
{
    public class CityRepository : ICityRepository
    {
        private readonly AppDbContext _appDbContext;

        public CityRepository(AppDbContext _appDbContext)
        {
            this._appDbContext = _appDbContext;
        }

        public async Task<bool> StateExist(int StateId)
        {
            var exist = await _appDbContext.States.AnyAsync(s => s.Id == StateId);
            if (exist)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<City>> GetCityList(int StateId)
        {
            return await _appDbContext.Cities.Where(c => c.StateId == StateId).ToListAsync();
        }

        
    }
}
