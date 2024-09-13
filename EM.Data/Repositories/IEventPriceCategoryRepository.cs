using EM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Repositories
{
    public interface IEventPriceCategoryRepository
    {
        public Task<EventTicketCategory> AddEventPriceCategory(EventTicketCategory eventTicketCategory);

        public Task<EventTicketCategory> UpdateEventPriceCategory(EventTicketCategory eventTicketCategory);

        public Task<EventTicketCategory> GetEventPriceCategoryById(int id);

        public Task<Event> EventExistance(int EventId);
    }
}
