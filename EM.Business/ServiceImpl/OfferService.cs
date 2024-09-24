using AutoMapper;
using EM.Business.BOs;
using EM.Business.Exceptions;
using EM.Business.Services;
using EM.Core.DTOs.Request;
using EM.Core.Helpers;
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
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _offerRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IVenueRepository _venueRepository;
        private readonly IMapper _mapper;
        public OfferService(IOfferRepository offerRepository, IEventRepository eventRepository, IVenueRepository venueRepository,IMapper mapper)
        {
            _offerRepository = offerRepository;
            _eventRepository = eventRepository;
            _venueRepository = venueRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="offerDto"></param>
        /// <param name="eventId"></param>
        /// <param name="offerId"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<OfferBO> AddUpdateEventOffer(OfferDTO offerDto, int eventId, int offerId)
        {

            await ValidateEventExists(eventId);
            await ValidateEventNotPublished(eventId);
            await ValidateEarlyBird(offerDto,eventId);
            await ValidateCapacity(offerDto, eventId);
            var offerBo = new OfferBO();
            if (offerId == 0 || offerId == null)
            {  
                offerBo = await AddEventOffer(offerDto, eventId);
            }
            else
            {
                offerBo = await UpdateEventOffer(offerDto, eventId, offerId);
            }
            return offerBo;
        }

        public async Task<OfferBO> AddEventOffer(OfferDTO offerDto, int eventId)
        {
            try {
                var offer = new EventOffer();
                offer.EventId = eventId;
                offer.CreatedOn = DateTime.UtcNow;
                offer.ModifiedOn = DateTime.UtcNow;
                _mapper.Map(offerDto, offer);
                var createdOffer = await _offerRepository.AddEventOffer(offer, eventId);
                var offerBo = _mapper.Map<OfferBO>(createdOffer);
                //offerBo.CreatedOn = TimeConversionHelper.ConvertFromUTCAndTruncate(offerBo.CreatedOn);
                //offerBo.ModifiedOn = TimeConversionHelper.ConvertFromUTCAndTruncate(offerBo.ModifiedOn);
                return offerBo;
            }catch(Exception ex)
            {
                throw new Exception("Error while adding offer", ex);
            }
            
        }

        public async Task<OfferBO> UpdateEventOffer(OfferDTO offerDto, int eventId, int offerId)
        {
            await ValidateOfferExists(offerDto,eventId);
            var offer = new EventOffer();
            offer.EventId = eventId;
            offer.ModifiedOn = DateTime.UtcNow;
            _mapper.Map(offerDto, offer);
            var updatedOffer = await _offerRepository.UpdateEventOffer(offer, eventId, offerId);
            var offerBo = _mapper.Map<OfferBO>(updatedOffer);
            offerBo.CreatedOn = TimeConversionHelper.ConvertTimeFromUTC(offerBo.CreatedOn);
            offerBo.ModifiedOn = TimeConversionHelper.ConvertTimeFromUTC(offerBo.ModifiedOn);
            offerBo.CreatedOn = TimeConversionHelper.TruncateSeconds(offerBo.CreatedOn);
            offerBo.ModifiedOn = TimeConversionHelper.TruncateSeconds(offerBo.ModifiedOn);
            return offerBo;
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
        /// Validating the Early Bird offer
        /// </summary>
        /// <param name="offer"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        /// <exception cref="EarlyBirdOfferExistsException"></exception>
        private async Task ValidateEarlyBird(OfferDTO offer,int eventId)
        {
            if (offer.OfferId == 0 && offer.Type == 0)
            {
                var exists =  await _offerRepository.EarlyBirdExists(eventId);
                if (exists)
                    throw new EarlyBirdOfferExistsException("EarlyBird offer already exists on the event.");
            }
        }
        /// <summary>
        /// Validate existing offer for update
        /// </summary>
        /// <param name="offerDTO"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        private async Task ValidateOfferExists(OfferDTO offerDTO,int eventId)
        {
            var offerExists = await _offerRepository.EventOfferExistsAsync(eventId,offerDTO.OfferId, (int)offerDTO.Type);
            if (!offerExists)
            {
                throw new NotFoundException("Offer");
            }
        }
        /// <summary>
        /// Validate the offer capacity
        /// </summary>
        /// <param name="offerDTO"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task ValidateCapacity(OfferDTO offerDTO, int eventId)
        {
            var _event = _eventRepository.GetEventById(eventId);
            var venue_id = _event.VenueId;
            var venue = await _venueRepository.GetVenueById(venue_id);
            if (offerDTO.Quantity > venue.MaxCapacity || offerDTO.GroupSize > venue.MaxCapacity)
            {
                throw new Exception("Offer quantity or group size should not exceed the venue capacity.");
            }
        }
    }
}
