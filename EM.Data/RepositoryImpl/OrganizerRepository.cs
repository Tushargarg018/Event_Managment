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
    public class OrganizerRepository : IOrganizerRepository
    {
        private readonly AppDbContext context;

        public OrganizerRepository(AppDbContext dbContext)
        {
            context = dbContext;
        }

        public async Task<Organizer> GetOrganizerByEmail(string email)
        {
            var organizer = await context.Organizers.FirstOrDefaultAsync(x => x.Email == email);
            return organizer;
        }

        public Organizer GetOrganizerByEmailAndPassword(string email, string password)
        {
            var organizer =  context.Organizers.FirstOrDefault(o => o.Email == email && o.Password == password);
            return organizer;
        }

        public async Task<IEnumerable<Organizer>> GetOrganizers()
        {
            var list = await context.Organizers.ToListAsync();
            return list;
        }

        public async Task<Organizer> GetOrganizerById(int id)
        {
            var organizer = await context.Organizers.FirstOrDefaultAsync(o=>o.Id == id);
            return organizer;
        }
    }
}
