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
        public EventOffer AddEventOffer(EventOffer offer, int eventId);
        public EventOffer UpdateEventOffer(EventOffer offer, int eventId, int offerId);
    }
}
