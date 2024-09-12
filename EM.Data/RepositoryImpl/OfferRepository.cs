using EM.Core.Enums;
using EM.Data.Entities;
using EM.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.RepositoryImpl
{
    public class OfferRepository : IOfferRepository
    {
        private readonly AppDbContext _context;
        public OfferRepository(AppDbContext dbContext)
        {
            _context = dbContext;
        }
        public EventOffer AddEventOffer(EventOffer offer, int eventId)
        {
            var lastId = _context.EventOffers
                             .OrderByDescending(p => p.Id)
                             .Select(p => p.Id)
                             .FirstOrDefault();
            var offerId = lastId + 1;
            offer.Id = offerId;
            offer.CreatedOn = DateTime.UtcNow;
            offer.ModifiedOn = DateTime.UtcNow;
            //var _event = _context.Events.FirstOrDefault(e=>e.Id == eventId);
            _context.EventOffers.Add(offer);
            _context.SaveChangesAsync();
            return offer;
        }
        public EventOffer UpdateEventOffer(EventOffer offer, int eventId, int offerId)
        {
            var existingOffer = _context.EventOffers.FirstOrDefaultAsync(o => o.Id == offerId).Result;
            offer.Id = offerId;
            existingOffer.TotalOffers = offer.TotalOffers;
            existingOffer.GroupSize = offer.GroupSize;
            existingOffer.Discount = offer.Discount;
            _context.SaveChangesAsync();
            return existingOffer;
        }

        public async Task<bool> EarlyBirdExists(int eventId)
        {
            return await _context.EventOffers.AnyAsync(o => o.EventId == eventId && o.Type == Core.Enums.OfferTypeEnum.EarlyBird);
        }

        public async Task<bool> OfferExistsAsync(int offerId, int offerType)
        {
            return await _context.EventOffers.AnyAsync(o => o.Id == offerId && (int)o.Type == offerType);
        }
    }
}
