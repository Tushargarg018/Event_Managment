using EM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Repositories
{
    public interface IPerformerRepository
    {
        public Task<Performer> AddPerformer(Performer performer);
        public IEnumerable<Performer> GetPerformersUsingOrganizer(int organizerId);
        public Task<bool> PerformerExistsAsync(int performerId);
        public Task<Performer> UpdatePerformer(string bio, string profile_pic, int performer_id);
        public Task<Performer> GetPerformerById(int id);
    }
}
