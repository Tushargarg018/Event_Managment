using AutoMapper;
using EM.Business.BOs;
using EM.Business.Services;
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
        public EventBO AddEvent(EventBO eventBO)
        {
            eventBO.Status = Core.Enums.StatusEnum.Draft;
            eventBO.CreatedOn = DateTime.UtcNow;
            eventBO.ModifiedOn = DateTime.UtcNow;
            var createEvent = new Event();
            _mapper.Map(eventBO, createEvent);
            createEvent.EventDocuments = new List<EventDocument>();
            createEvent.EventOffers = [];
            createEvent.EventTicketCategories = new List<EventTicketCategory>();
            var responseEvent = _eventRepository.AddEvent(createEvent);
            _mapper.Map(responseEvent, eventBO);
            eventBO.Status = Core.Enums.StatusEnum.Draft;
            return eventBO;
        }
    }
}
