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
            IQueryable<Event> query = context.Set<Event>()
                                            .Include(e => e.Performer)
                                            .Include(e => e.EventDocuments)
                                            .Include(e => e.EventOffers)
                                            .Include(e => e.EventTicketCategories)
                                            .Include(e => e.Venue)
                                            .ThenInclude(v => v.State)
                                            .Include(e => e.Venue)
                                            .ThenInclude(v => v.City);
            return await query.FirstOrDefaultAsync(e=>e.Id == eventId);
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
                                            .Include(e=> e.EventOffers)
                                            .Include(e=> e.EventTicketCategories)
                                            .Include(e => e.Venue)
                                            .ThenInclude(v => v.State)
                                            .Include(e => e.Venue)
                                            .ThenInclude(v=>v.City);

            if(filter.StartDateTime.HasValue)
                query = query.Where(e => e.StartDatetime >= filter.StartDateTime.Value.ToUniversalTime());
            if (filter.EndDateTime.HasValue)
            {
                //var time = filter.EndDateTime.Value.ToUniversalTime();
                //var time = TimeConversionHelper.ConvertISTToUTC(filter.EndDateTime.Value.ToString());
                query = query.Where(e => e.EndDatetime <= filter.EndDateTime.Value.ToUniversalTime());
            }

            if (!string.IsNullOrEmpty(filter.Title))
                query = query.Where(e => e.Title.ToLower() == filter.Title.ToLower());

            if (filter.Status >= 0)  
                query = query.Where(e => (int)e.Status == filter.Status);

            //if (!string.IsNullOrEmpty(filter.EndDateTime))
            //    query = query.Where(e => e.EndDatetime >= DateTime.Parse(filter.EndDateTime));


            if (filter.OrganizerId.HasValue)
                query = query.Where(e => e.OrganizerId == filter.OrganizerId);

            if (filter.SortBy != null && (filter.Sort !=null && (filter.Sort=="Ascending" || filter.Sort=="Descending")))
            {
                if (filter.SortBy == "start_date_time")
                {
                    if (filter.Sort == "asc")
                    {
                        query = query.OrderBy(e => e.StartDatetime);
                    }
                    else
                        query = query.OrderByDescending(e => e.StartDatetime);
                }
                else if(filter.SortBy == "end_date_time")
                {
                    if (filter.Sort == "desc")
                    {
                        query = query.OrderBy(e => e.EndDatetime);
                    }
                    else
                        query = query.OrderByDescending(e => e.EndDatetime);
                }
            }

            // Pagination
            var totalRecords = await query.CountAsync();
            var events = await query.Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();
            foreach(var _event in events)
            {
                _event.CreatedOn = TimeConversionHelper.ConvertTimeFromUTC(_event.CreatedOn);
                _event.ModifiedOn = TimeConversionHelper.ConvertTimeFromUTC(_event.ModifiedOn);
                _event.CreatedOn = TimeConversionHelper.TruncateSeconds(_event.CreatedOn);
                _event.ModifiedOn = TimeConversionHelper.TruncateSeconds(_event.ModifiedOn);

                _event.StartDatetime = TimeConversionHelper.ConvertTimeFromUTC(_event.StartDatetime);
                _event.StartDatetime = TimeConversionHelper.TruncateSeconds(_event.StartDatetime);
                _event.EndDatetime = TimeConversionHelper.ConvertTimeFromUTC(_event.EndDatetime);
                _event.EndDatetime = TimeConversionHelper.TruncateSeconds(_event.EndDatetime);
            }
            return (events, totalRecords);
        }
    }
}
