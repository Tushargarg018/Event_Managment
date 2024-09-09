using EM.Business.Repository;
using EM.Data;
using EM.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.Services
{
    public class StateRepository : IStateRepository
    {
        private readonly AppDbContext appDbContext;
        public StateRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<State>> GetStateList(int CountryId)
        {
            return await appDbContext.States.Where(s=>s.CountryId== CountryId).ToListAsync();
        }

		public async Task<bool> StateExistsAsync(int stateId)
		{
			return await appDbContext.States.AnyAsync(s => s.Id == stateId);
		}
	}
}
