using EM.Core.DTOs.Request;
using EM.Core.DTOs.Response;
using EM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Repositories
{
    public interface IEventRepository
    {
        public Task<Event> AddEvent(Event eventToAdd);
        public Task<bool> EventExistsAsync(int eventId);
        public Task<bool> EventNotPublished(int eventId);
        public Event GetEventById(int eventId);
        public Task<Event> GetEventByIdAsync(int eventId);

        public Task<(List<Event> Events, int TotalCount)> GetEventsAsync(EventFilterDTO filter);

        public Task<Event> PublishEvent(Event e);

        public Task<Event> UpdateEvent(Event eventToUpdate);

        public Task<List<Event>> GetEventsByVenue(int id, DateTime StartDateTime, DateTime EndDateTime, int eventId);
        public Task<List<Event>> GetEventsByPerformer(int id, DateTime startDateTime, DateTime endDateTime, int eventId);

        public Task<TaxConfiguration> GetTaxConfigurationById(int CountryId , int? stateId = null);
    }   
}
