using EM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Repositories
{
    public interface IEventRepository
    {
        public Task<Event> AddEvent(Event eventToAdd);
    }
}
