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
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _offerRepository;
        public OfferService(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }
        public EventOffer AddEventOffer(EventOffer offer, int eventId)
        {
            return _offerRepository.AddEventOffer(offer, eventId);
        }
        public EventOffer UpdateEventOffer(EventOffer offer, int eventId, int offerId)
        {
            return _offerRepository.UpdateEventOffer(offer, eventId, offerId);
        }
    }
}
