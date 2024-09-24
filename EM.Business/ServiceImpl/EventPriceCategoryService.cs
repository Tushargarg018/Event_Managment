using AutoMapper;
using EM.Business.BOs;
using EM.Business.Exceptions;
using EM.Business.Services;
using EM.Core.DTOs.Request;
using EM.Data.Entities;
using EM.Data.Repositories;
using EM.Data.Repository;
using EM.Data.RepositoryImpl;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.ServiceImpl
{
    public class EventPriceCategoryService : IEventPriceCategoryService
    {
        private readonly IEventPriceCategoryRepository repository;
        private readonly IEventRepository _eventRepository;
        private readonly IVenueRepository _venueRepository;
        private readonly IMapper mapper;

        public EventPriceCategoryService(IEventPriceCategoryRepository repository , IEventRepository eventRepository, IVenueRepository venueRepository, IMapper mapper)
        {
            this.repository = repository;
            _eventRepository = eventRepository;
            _venueRepository = venueRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// To process the ticket category addition and updating
        /// </summary>
        /// <param name="eventPriceCategoryRequestDTO"></param>
        /// <returns></returns>
        public async Task<EventPriceCategoryBO> AddorUpdateEventPriceCategory(EventPriceCategoryRequestDTO eventPriceCategoryRequestDTO)
        {

            await ValidateEventExists(eventPriceCategoryRequestDTO.EventId);
            await ValidateEventNotPublished(eventPriceCategoryRequestDTO.EventId);
            await ValidateCapacity(eventPriceCategoryRequestDTO);
            if (eventPriceCategoryRequestDTO.Id == null)
            {
                return await AddEventPriceCategory(eventPriceCategoryRequestDTO);
            }
            else
            {
                return await UpdateEvenPriceCategory(eventPriceCategoryRequestDTO);
            }

        }
        /// <summary>
        /// Process the ticket category add request
        /// </summary>
        /// <param name="eventPriceCategoryRequestDTO"></param>
        /// <returns></returns>

        private async Task<EventPriceCategoryBO> AddEventPriceCategory(EventPriceCategoryRequestDTO eventPriceCategoryRequestDTO)
        {
            EventTicketCategory ticketCategory = new EventTicketCategory
            {
                Name = eventPriceCategoryRequestDTO.Name,
                EventId = eventPriceCategoryRequestDTO.EventId,
                Price = eventPriceCategoryRequestDTO.Price,
                Capacity = eventPriceCategoryRequestDTO.Capacity,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
            };

            EventTicketCategory eventTicket = await repository.AddEventPriceCategory(ticketCategory);
            EventPriceCategoryBO eventPriceCategoryBO = new EventPriceCategoryBO();
            mapper.Map(eventTicket, eventPriceCategoryBO );
            return eventPriceCategoryBO;
        }
        /// <summary>
        /// To process the ticket category update request
        /// </summary>
        /// <param name="eventPriceCategoryRequestDTO"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<EventPriceCategoryBO> UpdateEvenPriceCategory(EventPriceCategoryRequestDTO eventPriceCategoryRequestDTO)
        {
            EventTicketCategory eventTicket = await repository.GetEventPriceCategoryById((int)eventPriceCategoryRequestDTO.Id);
            if (eventTicket == null)
            {
                throw new Exception("Event Ticket Category does not exist");
            }
            eventTicket.Name = eventPriceCategoryRequestDTO.Name;
            eventTicket.Price = eventPriceCategoryRequestDTO.Price;
            eventTicket.Capacity = eventPriceCategoryRequestDTO.Capacity;
            eventTicket.ModifiedOn = DateTime.UtcNow;

            EventTicketCategory eventTicketCategory = await repository.UpdateEventPriceCategory(eventTicket);
            EventPriceCategoryBO eventPriceCategoryBO = new EventPriceCategoryBO();
            mapper.Map(eventTicketCategory, eventPriceCategoryBO);
            return eventPriceCategoryBO;
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
        /// <summary>
        /// Validate the ticket category update request
        /// </summary>
        /// <param name="eventPriceCategoryRequestDTO"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task ValidateEventPriceCategoryUpdate(EventPriceCategoryRequestDTO eventPriceCategoryRequestDTO)
        {
            var eventTicket = await repository.GetEventPriceCategoryById((int)eventPriceCategoryRequestDTO.Id);
            if (eventTicket == null)
            {
                throw new Exception("Event Ticket Category does not exist");
            }
        }
        /// <summary>
        /// Validate the capacity of the event
        /// </summary>
        /// <param name="eventPriceCategoryDTO"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task ValidateCapacity(EventPriceCategoryRequestDTO eventPriceCategoryDTO)
        {
            var eventDetails = await _eventRepository.GetEventByIdAsync(eventPriceCategoryDTO.EventId);
            var venueCapacity = await _venueRepository.GetVenueCapacityByIdAsync(eventDetails.VenueId);

            var totalAllocatedCapacity = await repository.GetTotalAllocatedSeatCapacityAsync(eventPriceCategoryDTO.EventId);

            if (eventPriceCategoryDTO.Id != null)
            {
                var existingCategoryCapacity = await repository.GetCategoryCapacityByIdAsync(eventPriceCategoryDTO.Id);
                totalAllocatedCapacity -= existingCategoryCapacity;
            }

            if(totalAllocatedCapacity + eventPriceCategoryDTO.Capacity > venueCapacity)
            {
                throw new Exception("Seat category capacity exceeds available capacity for the event");
            }
        }
    }
}
