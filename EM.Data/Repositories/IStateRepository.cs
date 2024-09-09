using EM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Repositories
{
    public interface IStateRepository
    {
        public Task<IEnumerable<State>> GetStateList(int CountryId);
        Task<bool> StateExistsAsync(int stateId);
    }
}
