using EM.Data.Entities;
using EM.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.RepositoryImpl
{
    public class PerformerRepository : IPerformerRepository
    {
        private readonly AppDbContext context;

        public PerformerRepository(AppDbContext dbContext)
        {
            context = dbContext;
        }
        public Performer AddPerformer(string name, string bio, string profile_pic, int organizer_id)
        {
            var lastId = context.Performers
                              .OrderByDescending(p => p.Id)
                              .Select(p => p.Id)
                              .FirstOrDefault();
            var performer_id = lastId + 1;
            var performer = new Performer
            {
                Id = performer_id,
                Name = name,
                Bio = bio,
                Profile = profile_pic,
                OrganizerId = organizer_id,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow
            };
            context.Performers.Add(performer);
            context.SaveChangesAsync();
            return performer;
        }

        public IEnumerable<Performer> GetPerformersUsingOrganizer(int organizerId)
        {
            var performer = context.Performers.Where(p=>p.OrganizerId == organizerId).ToList();
            return performer;  
        }

        public async Task<bool> PerformerExistsAsync(int performerId)
        {
            return await context.Performers.AnyAsync(p => p.Id == performerId);
        }

    }
}
