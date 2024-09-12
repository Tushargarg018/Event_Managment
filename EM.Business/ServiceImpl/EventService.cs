using AutoMapper;
using EM.Business.BOs;
using EM.Business.Services;
using EM.Core.DTOs.Request;
using EM.Core.Helpers;
using EM.Data.Entities;
using EM.Data.Repositories;
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
        public EventBO AddEvent(EventDTO eventDto, int organizerId)
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
            var _event = _eventRepository.AddEvent(createEvent);
            var eventBo = new EventBO();
            _mapper.Map(_event, eventBo);
            eventBo.CreatedOn = TimeConversionHelper.ConvertTimeFromUTC(eventBo.CreatedOn);
            eventBo.ModifiedOn = TimeConversionHelper.ConvertTimeFromUTC(eventBo.ModifiedOn);
            return eventBo;
        }
    }
}
