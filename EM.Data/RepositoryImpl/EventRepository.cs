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
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext context;

        public EventRepository(AppDbContext dbContext)
        {
            context = dbContext;
        }
        public async Task<Event> AddEvent(Event eventToAdd)
        {
            await context.AddAsync(eventToAdd);
            await context.SaveChangesAsync();
            return eventToAdd;
        }
    }
}
