using AutoMapper;
using EM.Business.BOs;
using EM.Business.Services;
using EM.Core.DTOs.Request;
using EM.Data.Entities;
using EM.Data.Repositories;
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
        private readonly IMapper mapper;

        public EventPriceCategoryService(IEventPriceCategoryRepository repository , IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<EventPriceCategoryBO> AddorUpdateEventPriceCategory(EventPriceCategoryRequestDTO eventPriceCategoryRequestDTO, int EventId)
        {
            /*var EventCheck = await repository.EventExistance(EventId);
            if (EventCheck == null)
            {
                return null;
            }*/

            if(eventPriceCategoryRequestDTO.Id == null)
            {
                return await AddEventPriceCategory(eventPriceCategoryRequestDTO ,  EventId);
            }
            else
            {
                return await UpdateEvenPriceCategory(eventPriceCategoryRequestDTO ,  EventId);
            }

        }



        private async Task<EventPriceCategoryBO> AddEventPriceCategory(EventPriceCategoryRequestDTO eventPriceCategoryRequestDTO  , int eventId)
        {
            EventTicketCategory ticketCategory = new EventTicketCategory
            {
                Name = eventPriceCategoryRequestDTO.Name,
                EventId = eventId,
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
        private async Task<EventPriceCategoryBO> UpdateEvenPriceCategory(EventPriceCategoryRequestDTO eventPriceCategoryRequestDTO, int eventId)
        {
            EventTicketCategory eventTicket = await repository.GetEventPriceCategoryById((int)eventPriceCategoryRequestDTO.Id);
            if (eventTicket == null)
            {
                throw new Exception("Event Ticket Category doesnot exist");
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
    }
}
