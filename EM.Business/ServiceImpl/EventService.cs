using AutoMapper;
using EM.Business.BOs;
using EM.Business.Services;
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
        public EventBO AddEvent(EventBO eventBO)
        {
            //Converting string to datetime objects
            string startDateString = eventBO.StartDateTime;
            string endDateString = eventBO.EndDateTime;
            
            eventBO.Status = Core.Enums.StatusEnum.Draft;
            eventBO.CreatedOn = DateTime.UtcNow;
            eventBO.ModifiedOn = DateTime.UtcNow;
            var createEvent = new Event();

            //Setting the conerted string dates inside Event entity
            createEvent.StartDatetime = TimeConversionHelper.ConvertISTToUTC(startDateString);
            createEvent.EndDatetime = TimeConversionHelper.ConvertISTToUTC(endDateString);

            //_mapper.Map(eventBO, createEvent);
            createEvent.Title = eventBO.Title;
            createEvent.Description = eventBO.Description;
            createEvent.BasePrice = eventBO.BasePrice;
            createEvent.OrganizerId = eventBO.OrganizerId;
            createEvent.CreatedOn = eventBO.CreatedOn;
            createEvent.ModifiedOn = eventBO.ModifiedOn;
            createEvent.PerformerId = eventBO.PerformerId;
            createEvent.VenueId = eventBO.VenueId;
            createEvent.EventDocuments = new List<EventDocument>();
            createEvent.EventOffers = [];
            createEvent.EventTicketCategories = new List<EventTicketCategory>();
            var responseEvent = _eventRepository.AddEvent(createEvent);
            //_mapper.Map(responseEvent, eventBO);
            //Created Modified need to be converted in IST string 
            eventBO.CreatedOn = TimeConversionHelper.ConvertTimeFromUTC(responseEvent.CreatedOn);
            eventBO.ModifiedOn = TimeConversionHelper.ConvertTimeFromUTC(responseEvent.ModifiedOn);
            return eventBO;
        }
    }
}
