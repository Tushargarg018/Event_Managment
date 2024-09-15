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
        public async Task<EventOffer> AddEventOffer(EventOffer offer, int eventId)
        {
            await _context.EventOffers.AddAsync(offer);
            await _context.SaveChangesAsync();
            return offer;
        }
        public async Task<EventOffer> UpdateEventOffer(EventOffer offer, int eventId, int offerId)
        {
            var existingOffer = _context.EventOffers.FirstOrDefaultAsync(o => o.Id == offerId).Result;
            existingOffer.TotalOffers = offer.TotalOffers;
            existingOffer.GroupSize = offer.GroupSize;
            existingOffer.Discount = offer.Discount;
            await _context.SaveChangesAsync();
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
