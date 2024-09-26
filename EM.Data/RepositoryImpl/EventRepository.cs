using EM.Core.DTOs.Request;
using EM.Core.DTOs.Response;
using EM.Core.Helpers;
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

        public async Task<bool> EventExistsAsync(int eventId)
        {
            return await context.Events.AnyAsync(e=>e.Id == eventId);
        }

        public async Task<bool> EventNotPublished(int eventId)
        {
            return await context.Events.AnyAsync(e => e.Id == eventId && e.Status == Core.Enums.StatusEnum.Draft);
        }

        public Event GetEventById(int eventId)
        {
            return context.Events.FirstOrDefault(e => e.Id == eventId);
        }

        public async Task<Event> GetEventByIdAsync(int eventId) {
            return await context.Set<Event>()
                                .Include(e => e.Performer)
                                .Include(e => e.EventDocuments)
                                .Include(e => e.EventOffers)
                                .Include(e => e.EventTicketCategories)
                                .Include(e => e.Venue)
                                    .ThenInclude(v => v.State) 
                                    
                                .Include(e => e.Venue)
                                    .ThenInclude(v => v.City)  
                                .FirstOrDefaultAsync(e => e.Id == eventId);
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
                                            .Include(e => e.Performer)
                                            .Include(e => e.EventDocuments)
                                            .Include(e => e.EventOffers)
                                            .Include(e => e.EventTicketCategories)
                                            .Include(e => e.Venue)
                                                .ThenInclude(v => v.City)
                                            .Include(e => e.Venue)
                                                .ThenInclude(v => v.State);                                            
            if(filter.StartDateTime.HasValue)
                query = query.Where(e => e.StartDatetime >= filter.StartDateTime.Value.ToUniversalTime());
            if (filter.EndDateTime.HasValue)
            {
                query = query.Where(e => e.EndDatetime <= filter.EndDateTime.Value.ToUniversalTime());
            }

            if (!string.IsNullOrEmpty(filter.Title))
                query = query.Where(e => e.Title.ToLower() == filter.Title.ToLower());

            if (filter.Status != null && (filter.Status == 0 || filter.Status == 1))
                query = query.Where(e => (int)e.Status == filter.Status);

            if (filter.OrganizerId.HasValue)
                query = query.Where(e => e.OrganizerId == filter.OrganizerId);

            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                bool isAscending = string.Equals(filter.SortOrder, "asc", StringComparison.OrdinalIgnoreCase);
                switch (filter.SortBy.ToLower())
                {
                    case "start_datetime":
                        query = isAscending ? query.OrderBy(e => e.StartDatetime) : query.OrderByDescending(e => e.StartDatetime);
                        break;
                    case "end_datetime":
                        query = isAscending ? query.OrderBy(e => e.EndDatetime) : query.OrderByDescending(e => e.EndDatetime);
                        break;
                    case "title":
                        query = isAscending ? query.OrderBy(e => e.Title) : query.OrderByDescending(e => e.Title);
                        break;
                    default:
                        query = query.OrderBy(e => e.StartDatetime);
                        break;
                }
            }
            var totalRecords = await query.CountAsync();
            var events = await query.Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();
            return (events, totalRecords);
        }

        public async Task<TaxConfiguration> GetTaxConfigurationById(int countryId, int? stateId = null)
        {
            IQueryable<TaxConfiguration> query = context.TaxConfigurations;
            if (countryId == 1)
            {
                query = query.Where(tc => tc.CountryId == countryId);
            }
            else
            {
                query = query.Where(tc => tc.CountryId == countryId && tc.StateId == stateId);
            }
            var taxConfig = await query.FirstOrDefaultAsync();
            return taxConfig;
        }

        public async Task<Event> PublishEvent(Event e)
        {
            context.Update(e);
            await context.SaveChangesAsync();
            return e;
        }

        public async Task<Event> UpdateEvent(Event eventToUpdate)
        {
            context.Update(eventToUpdate);
            await context.SaveChangesAsync();
            return eventToUpdate;
        }

        public async Task<List<Event>> GetEventsByVenue(int id, DateTime startDateTime, DateTime endDateTime, int eventId)
        {
            return await context.Events.Where(e => (e.Id != eventId && e.Status == Core.Enums.StatusEnum.Published && e.VenueId == id && (e.StartDatetime<=endDateTime && e.EndDatetime>=startDateTime))).ToListAsync();
        }

        //eventId for the same event (when Updating)
        public async Task<List<Event>> GetEventsByPerformer(int id, DateTime startDateTime, DateTime endDateTime, int eventId)
        {
            return await context.Events.Where(e => (e.Id != eventId && e.Status == Core.Enums.StatusEnum.Published && e.PerformerId == id && (e.StartDatetime <= endDateTime && e.EndDatetime >= startDateTime))).ToListAsync();
        }
    }
}
