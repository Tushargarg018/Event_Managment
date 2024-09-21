using EM.Business.BOs;
using EM.Core.DTOs.Request;
using EM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.Services
{
    public interface IOfferService
    {
        public Task<OfferBO> AddEventOffer(OfferDTO offerDto, int eventId);
        public Task<OfferBO> UpdateEventOffer(OfferDTO offerDto, int eventId, int offerId);
        public Task<OfferBO> AddUpdateEventOffer(OfferDTO offerDto, int eventId, int offerId);
    }
}
