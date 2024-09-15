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

        public Event GetEventById(int eventId)
        {
            var eventResult = context.Events.FirstOrDefault(e=>e.Id == eventId);
            return eventResult;
        }

        public async Task<bool> EventExistsAsync(int eventId)
        {
            return await context.Events.AnyAsync(e=>e.Id == eventId);
        }

        public async Task<bool> EventNotPublished(int eventId)
        {
            return await context.Events.AnyAsync(e => e.Id == eventId && e.Status == Core.Enums.StatusEnum.Draft);
        }

        public async Task<Event> GetEventByIdAsync(int eventId) {
            return await context.Events.FirstOrDefaultAsync(e => e.Id == eventId);
        }

    }
}
