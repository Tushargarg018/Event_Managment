using EM.Core.DTOs.Request;
using EM.Core.Enums;
using EM.Data.Repositories;
using EM.Data.Repository;
using EM.Data.RepositoryImpl;
using FluentValidation;

namespace EM.Api.Validations
{
    public class OfferValidator : AbstractValidator<OfferDTO>
    {
        IVenueRepository _venueRepository;
        IEventRepository _eventRepository;
        IOfferRepository _offerRepository;
        public OfferValidator(IVenueRepository venueRepository, IEventRepository eventRepository, IOfferRepository offerRepository)
        {
            _venueRepository = venueRepository;
            _eventRepository = eventRepository;
            _offerRepository = offerRepository;

            RuleFor(o => o.Discount)
                .GreaterThan(0)
                .LessThan(100)
                .WithMessage("Invalid Offer Discount. Range Must be from 0.00 to 99.99");

            RuleFor(o => o.EventId)
                .MustAsync(EventExistsAsync).WithMessage("Event Does not exist")
                .MustAsync(EventNotPublished).WithMessage("Event is Already Published");

            RuleFor(o => o)
                .Must(ValidateEarlyBird).WithMessage("Early Bird Offer Already Exists")
                .Must(ValidateOffer).WithMessage("Offer does not exist")
                .Must(ValidateTypeParameters).WithMessage("Can't have Group Size in Early Bird / Can't have Quantity in Group Offer")
                .Must(ValidateCapacity).WithMessage("Offer Size can't exceed venue capacity");

        }

        private bool ValidateEarlyBird(OfferDTO offer)
        {
            if(offer.OfferId == 0 && offer.Type == 0)
            {
                var exists = EarlyBirdExists(offer.EventId).Result;
                //early bird exists, so returning false
                if (exists)
                    return false;
            }
            return true;
        }

        private bool ValidateOffer(OfferDTO offer)
        {
           if(offer.OfferId == 0)
            {
                return true;
            }
            var exists = OfferExistsAsync(offer.OfferId, offer.Type).Result;
            if (exists) return true;
            return false;
        }

        private bool ValidateTypeParameters(OfferDTO offer)
        {
            if (offer.Type == (int)OfferTypeEnum.EarlyBird)
            {
                if (offer.GroupSize != 0)
                    return false;
                return true;
            }
            else
            {
                if (offer.Quantity != 0)
                    return false;
                return true;
            }
        }

        private bool ValidateCapacity(OfferDTO offer)
        {
            var capacity = GetVenueCapacity(offer.EventId);
            if(offer.Quantity > capacity || offer.GroupSize > capacity)
                return false;
            return true;
        }

        private async Task<bool> EventExistsAsync(int eventId, CancellationToken token)
        {
            return await _eventRepository.EventExistsAsync(eventId);
        }

        private async Task<bool> EventNotPublished(int eventId, CancellationToken token)
        {
            return await _eventRepository.EventNotPublished(eventId);
        }
        private async Task<bool> EarlyBirdExists(int eventId)
        {
            return await _offerRepository.EarlyBirdExists(eventId);
        }
        private async Task<bool> OfferExistsAsync(int offerId, int offerType)
        {
            return await _offerRepository.OfferExistsAsync(offerId, offerType);
        }
        private int GetVenueCapacity(int eventId)
        {
            var _event =  _eventRepository.GetEventById(eventId);
            var venue_id = _event.VenueId;
            var venue = _venueRepository.GetVenueById(venue_id).Result;
            return venue.MaxCapacity;
        }

    }
}
