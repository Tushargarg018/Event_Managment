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
        public OfferBO AddEventOffer(OfferDTO offerDto, int eventId);
        public OfferBO UpdateEventOffer(OfferDTO offerDto, int eventId, int offerId);
        public OfferBO AddUpdateEventOffer(OfferDTO offerDto, int eventId, int offerId);
    }
}
