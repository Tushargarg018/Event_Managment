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
    public class EventPriceCategoryRepository : IEventPriceCategoryRepository
    {

        private readonly AppDbContext appDbContext;

        public EventPriceCategoryRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<EventTicketCategory> AddEventPriceCategory(EventTicketCategory eventTicketCategory)
        {
            await appDbContext.AddAsync(eventTicketCategory);
            await appDbContext.SaveChangesAsync();
            return eventTicketCategory;
        }

        public async Task<Event> EventExistance(int EventId)
        {
            var existingEvent = await appDbContext.Events.FindAsync(EventId);
            if (existingEvent == null)
            {
                return null;
            }
            else
            {
                return existingEvent;
            }
        }

        public async Task<EventTicketCategory> GetEventPriceCategoryById(int id)
        {
            var Ticketid = await appDbContext.EventTicketCategories.FindAsync(id);
            if(Ticketid == null)
            {
                return null;
            }
            return Ticketid;
        }

        public async Task<int> GetTotalAllocatedSeatCapacityAsync(int eventId)
        {
            return await appDbContext.EventTicketCategories
                                     .Where(sc => sc.EventId == eventId)
                                     .SumAsync(sc=>sc.Capacity);
        }

        public async Task<int> GetCategoryCapacityByIdAsync(int? id) {
            var ticketCategory = await appDbContext.EventTicketCategories.FindAsync(id);
            return ticketCategory?.Capacity ?? 0;
        }

        public async Task<EventTicketCategory> UpdateEventPriceCategory(EventTicketCategory eventTicketCategory)
        {
            appDbContext.Update(eventTicketCategory);
            await appDbContext.SaveChangesAsync();
            return eventTicketCategory;
        }
    }
}
