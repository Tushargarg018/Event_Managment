using EM.Core.DTOs.Request;
using EM.Core.DTOs.Response;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<(List<Event> Events, int TotalCount)> GetEventsAsync(EventFilterDTO filter)
        {
            var pageSize = filter.Size;
            var pageIndex = filter.Page;
            IQueryable<Event> query = context.Set<Event>()
                                            .Include(e => e.Venue)
                                            .Include(e => e.Performer)
                                            .Include(e => e.EventDocuments)
                                            .Include(e=> e.EventOffers)
                                            .Include(e=> e.EventTicketCategories);

            if(filter.StartDateTime.HasValue)
                query = query.Where(e => e.StartDatetime >= filter.StartDateTime.Value.ToUniversalTime());
            if(filter.EndDateTime.HasValue)
                query = query.Where(e => e.EndDatetime <= filter.EndDateTime.Value.ToUniversalTime());

            if (!string.IsNullOrEmpty(filter.Title))
                query = query.Where(e => e.Title.ToLower() == filter.Title.ToLower());

            if (filter.Status >= 0)  
                query = query.Where(e => (int)e.Status == filter.Status);

            //if (!string.IsNullOrEmpty(filter.EndDateTime))
            //    query = query.Where(e => e.EndDatetime >= DateTime.Parse(filter.EndDateTime));


            if (filter.OrganizerId.HasValue)
                query = query.Where(e => e.OrganizerId == filter.OrganizerId);

            // Pagination
            var totalRecords = await query.CountAsync();
            var events = await query.Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            return (events, totalRecords);
        }
    }
}
