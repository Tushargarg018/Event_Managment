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
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _offerRepository;
        private readonly IMapper _mapper;
        public OfferService(IOfferRepository offerRepository, IMapper mapper)
        {
            _offerRepository = offerRepository;
            _mapper = mapper;
        }

        public OfferBO AddUpdateEventOffer(OfferDTO offerDto, int eventId, int offerId)
        {
            var offerBo = new OfferBO();
            if (offerId == 0 || offerId == null)
            {
                offerBo = AddEventOffer(offerDto, eventId);
            }
            else
            {
                offerBo = UpdateEventOffer(offerDto, eventId, offerId);
            }
            return offerBo;
        }

        public OfferBO AddEventOffer(OfferDTO offerDto, int eventId)
        {
            var offer = new EventOffer();
            offer.EventId = eventId;
            offer.CreatedOn = DateTime.UtcNow;
            offer.ModifiedOn = DateTime.UtcNow;
            _mapper.Map(offerDto, offer);
            var createdOffer =  _offerRepository.AddEventOffer(offer, eventId).Result;
            var offerBo = _mapper.Map<OfferBO>(createdOffer);
            offerBo.CreatedOn = TimeConversionHelper.ConvertTimeFromUTC(offerBo.CreatedOn);
            offerBo.ModifiedOn = TimeConversionHelper.ConvertTimeFromUTC(offerBo.ModifiedOn);
            offerBo.CreatedOn = TimeConversionHelper.TruncateSeconds(offerBo.CreatedOn);
            offerBo.ModifiedOn = TimeConversionHelper.TruncateSeconds(offerBo.ModifiedOn);
            return offerBo;
        }

        public OfferBO UpdateEventOffer(OfferDTO offerDto, int eventId, int offerId)
        {
            var offer = new EventOffer();
            offer.EventId = eventId;
            offer.ModifiedOn = DateTime.UtcNow;
            _mapper.Map(offerDto, offer);
            var updatedOffer = _offerRepository.UpdateEventOffer(offer, eventId, offerId).Result;
            var offerBo = _mapper.Map<OfferBO>(updatedOffer);
            offerBo.CreatedOn = TimeConversionHelper.ConvertTimeFromUTC(offerBo.CreatedOn);
            offerBo.ModifiedOn = TimeConversionHelper.ConvertTimeFromUTC(offerBo.ModifiedOn);
            offerBo.CreatedOn = TimeConversionHelper.TruncateSeconds(offerBo.CreatedOn);
            offerBo.ModifiedOn = TimeConversionHelper.TruncateSeconds(offerBo.ModifiedOn);
            return offerBo;
        }
    }
}
