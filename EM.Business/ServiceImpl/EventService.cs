using AutoMapper;
using EM.Business.BOs;
using EM.Business.Services;
using EM.Core.DTOs.Request;
using EM.Core.DTOs.Response;
using EM.Core.DTOs.Response.Success;
using EM.Core.Helpers;
using EM.Data.Entities;
using EM.Data.Repositories;
using EM.Data.RepositoryImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.ServiceImpl
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
     

        public EventService(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }
        public async Task<EventBO> AddEvent(EventDTO eventDto, int organizerId)
        {
            //Converting string to datetime objects
            string startDateString = eventDto.StartDateTime;
            string endDateString = eventDto.EndDateTime;
            var createEvent = new Event
            {
                Title = eventDto.Title,
                Description = eventDto.Description,
                BasePrice = eventDto.Price,
                OrganizerId = organizerId,
                PerformerId = eventDto.PerformerId,
                VenueId = eventDto.VenueId,
                Status = Core.Enums.StatusEnum.Draft,
                StartDatetime = TimeConversionHelper.ConvertISTToUTC(startDateString),
                EndDatetime = TimeConversionHelper.ConvertISTToUTC(endDateString),
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow
            };
            var createdEvent = await _eventRepository.AddEvent(createEvent);
            return _mapper.Map<EventBO>(createdEvent);
        }

        public async Task<EventBO> GetEventById(int eventId)
        {
            var _event = await _eventRepository.GetEventByIdAsync(eventId);
            var eventBo = _mapper.Map<EventBO>(_event);
            return eventBo;
        }

        public async Task<PagedEventBO> GetEventsAsync(EventFilterDTO filter)
        {
            var (events, totalRecords) = await _eventRepository.GetEventsAsync(filter);
            var eventBo = _mapper.Map<List<EventBO>>(events);

            return new PagedEventBO(eventBo,totalRecords);
        }
    }
}
