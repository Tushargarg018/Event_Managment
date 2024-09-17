using EM.Business.BOs;
using EM.Core.DTOs.Request;
using EM.Core.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.Services
{
    public interface IEventService
    {
        public Task<EventBO> AddEvent(EventDTO eventDto, int organizerId);
        Task<PagedEventBO> GetEventsAsync(EventFilterDTO filter);
    }
}
