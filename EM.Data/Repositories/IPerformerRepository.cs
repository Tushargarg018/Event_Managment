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
        public Performer AddPerformer(string name, string bio, string profile_pic, int organizer_id);
        public IEnumerable<Performer> GetPerformersUsingOrganizer(int organizerId);
        public Task<bool> PerformerExistsAsync(int performerId);

    }
}
