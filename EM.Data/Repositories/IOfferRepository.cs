using EM.Core.DTOs.Request;
using EM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Repositories
{
    public interface IOfferRepository
    {
        public Task<EventOffer> AddEventOffer(EventOffer offer, int eventId);
        public Task<EventOffer> UpdateEventOffer(EventOffer offer, int eventId, int offerId);
        public Task<bool> EarlyBirdExists(int eventId);
        public Task<bool> OfferExistsAsync(int offerId, int offerType);

        public Task<bool> EventOfferExistsAsync(int eventId,int offerId,int type);
    }
}
