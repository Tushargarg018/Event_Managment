﻿using AutoMapper;
using EM.Business.BOs;
using EM.Business.Exceptions;
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
using System.Text.Json;
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
        /// <summary>
        /// Add the event
        /// </summary>
        /// <param name="eventDto"></param>
        /// <param name="organizerId"></param>
        /// <returns></returns>
        public async Task<EventBO> AddEvent(EventDTO eventDto, int organizerId)
        {
            await ValidateVenueAvailability(eventDto.VenueId, eventDto.StartDateTime, eventDto.EndDateTime, 0);
            await ValidatePerformerAvailability(eventDto.PerformerId, eventDto.StartDateTime, eventDto.EndDateTime, 0);
            string startDateString = eventDto.StartDateTime;
            string endDateString = eventDto.EndDateTime;
            var createEvent = new Event
            {
                Title = eventDto.Title,
                Description = eventDto.Description,
                BasePrice = eventDto.Price,
                Currency = eventDto.Currency,
                OrganizerId = organizerId,
                PerformerId = eventDto.PerformerId,
                VenueId = eventDto.VenueId,
                Status = Core.Enums.StatusEnum.Draft,
                StartDatetime = TimeConversionHelper.ToUTCDateTime(startDateString),
                EndDatetime = TimeConversionHelper.ToUTCDateTime(endDateString),
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
                Flag  = eventDto.Flag
            };
            var createdEvent = await _eventRepository.AddEvent(createEvent);
            return _mapper.Map<EventBO>(createdEvent);
        }
        /// <summary>
        /// Update the event
        /// </summary>
        /// <param name="eventDto"></param>
        /// <param name="eventId"></param>
        /// <param name="organizerId"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<EventBO> UpdateEvent(EventDTO eventDto, int eventId, int organizerId)
        {
            await ValidateEventExists(eventId);
            await ValidateEventNotPublished(eventId);
            await ValidateVenueAvailability(eventDto.VenueId, eventDto.StartDateTime, eventDto.EndDateTime, eventId);
            await ValidatePerformerAvailability(eventDto.PerformerId, eventDto.StartDateTime, eventDto.EndDateTime, eventId);
            string startDateString = eventDto.StartDateTime;
            string endDateString = eventDto.EndDateTime;
            Event existingEvent = _eventRepository.GetEventById(eventId) ?? throw new NotFoundException("Event does not exists");
            existingEvent.Title = eventDto.Title;
            existingEvent.Description = eventDto.Description;
            existingEvent.BasePrice = eventDto.Price;
            existingEvent.Currency = eventDto.Currency;
            existingEvent.OrganizerId = organizerId;
            existingEvent.PerformerId = eventDto.PerformerId;
            existingEvent.VenueId = eventDto.VenueId;
            existingEvent.Status = Core.Enums.StatusEnum.Draft; 
            existingEvent.StartDatetime = TimeConversionHelper.ToUTCDateTime(startDateString);
            existingEvent.EndDatetime = TimeConversionHelper.ToUTCDateTime(endDateString);
            existingEvent.ModifiedOn = DateTime.UtcNow;
            existingEvent.Flag = eventDto.Flag;
            var createdEvent = await _eventRepository.UpdateEvent(existingEvent);
            var responseEvent =  _mapper.Map<EventBO>(createdEvent);
            return responseEvent;
        }
        /// <summary>
        /// Get the event by id
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<EventBO> GetEventById(int eventId)
        {
            var _event = await _eventRepository.GetEventByIdAsync(eventId) ?? throw new NotFoundException("Event");
            Venue venue = _event.Venue;
            TaxConfiguration taxDetail = await _eventRepository.GetTaxConfigurationById(venue.CountryId, venue.StateId);
            var eventBo = _mapper.Map<EventBO>(_event);
            if (_event.Flag == 0)
            {               
                eventBo.TaxDetail = JsonDocument.Parse("{}");
            }
            else if (taxDetail != null && taxDetail.TaxDetails != null)
            {       
                eventBo.TaxDetail = JsonDocument.Parse(taxDetail.TaxDetails.RootElement.ToString());
            }
            return eventBo;
        }
        /// <summary>
        /// Fetch the events
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<PagedEventBO> GetEventsAsync(EventFilterDTO filter)
        {
            var (events, totalRecords) = await _eventRepository.GetEventsAsync(filter);
            var eventBo = _mapper.Map<List<EventBO>>(events);



            return new PagedEventBO(eventBo, totalRecords);
        }
        /// <summary>
        /// Publish the event
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<EventBO> PublishEvent(int eventId)
        {
            await ValidateEventExists(eventId);
            await ValidateEventNotPublished(eventId);

            Event e = _eventRepository.GetEventById(eventId);

            string startDate = TimeConversionHelper.ToCustomDateTimeString(e.StartDatetime);
            string endDate = TimeConversionHelper.ToCustomDateTimeString(e.EndDatetime);

            await ValidateVenueAvailability(e.VenueId, startDate, endDate, eventId);
            await ValidatePerformerAvailability(e.PerformerId, startDate, endDate, eventId);
            DateTime dateTime = DateTime.UtcNow;
            if (e.StartDatetime <= dateTime)
            {
                throw new Exception("Can not publish an event after it has started.");
            }
            e.Status = Core.Enums.StatusEnum.Published;
            Event @event = await _eventRepository.PublishEvent(e);
            var eventBo = _mapper.Map<EventBO>(@event);
            return eventBo;

        }
        /// <summary>
        /// Check if event is exists
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        private async Task ValidateEventExists(int eventId)
        {
            var eventExists = await _eventRepository.EventExistsAsync(eventId);
            if (!eventExists)
            {
                throw new NotFoundException("Event");
            }
        }

        /// <summary>
        /// Check if event is not published
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        private async Task ValidateEventNotPublished(int eventId)
        {
            var eventNotPublished = await _eventRepository.EventNotPublished(eventId);
            if (!eventNotPublished)
            {
                throw new EventAlreadyPublishedException("Event is already published.");
            }
        }
        
        private async Task ValidateVenueAvailability(int venueId, string startDate, string endDate, int eventId)
        {
            DateTime startDateTime = TimeConversionHelper.ToUTCDateTime(startDate);
            DateTime endDateTime = TimeConversionHelper.ToUTCDateTime(endDate);
   
            List<Event> events = await _eventRepository.GetEventsByVenue(venueId, startDateTime, endDateTime, eventId);
            
                if (events.Count != 0)
                {
                    throw new VenueNotAvailableException("Venue not available on the mentioned dates");
                }
            
        }

        private async Task ValidatePerformerAvailability(int performerId, string startDate, string endDate, int eventId)
        {

            DateTime startDateTime = TimeConversionHelper.ToUTCDateTime(startDate);
            DateTime endDateTime = TimeConversionHelper.ToUTCDateTime(endDate);
            
            List<Event> events = await _eventRepository.GetEventsByPerformer(performerId, startDateTime, endDateTime, eventId);
            if (events.Count != 0)
            {
                throw new PerformerNotAvailableException("Performer not available on the mentioned dates");
            }
        }
    }
}
