using EM.Business.BOs;
using EM.Core.DTOs.Request;
using EM.Data;
using EM.Data.Entities;
using EM.Data.Repositories;
using EM.Data.Repository;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EM.Api.Validations
{
    public class EventPriceCategoryValidator : AbstractValidator<EventPriceCategoryRequestDTO>
    {
        private readonly IEventPriceCategoryRepository _eventRepository;
        private readonly IVenueRepository venueRepository;
        private readonly AppDbContext _appDbContext;
        public EventPriceCategoryValidator(IEventPriceCategoryRepository _eventRepository , IVenueRepository venueRepository , AppDbContext _appDbContext)
        {
            this._eventRepository  = _eventRepository;
            this.venueRepository = venueRepository;
            this._appDbContext =  _appDbContext;

            RuleFor(n => n.Name)
                .Length(1, 50).WithMessage("Name length limit exceeded");

            RuleFor(x => x.Capacity)
            .GreaterThan(0)
            .WithMessage("Capacity must be greater than 0.");

            RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0.");

            RuleFor(o => o)
                .CustomAsync(async (eventPriceCategory, context, cancellationToken) =>
                {
                    await ValidateCapacity(eventPriceCategory, context, cancellationToken);
                });
        }

        private async Task ValidateCapacity(EventPriceCategoryRequestDTO eventPriceCategory  , ValidationContext<EventPriceCategoryRequestDTO> context , CancellationToken cancellationToken)
        {
            Event events = await _eventRepository.EventExistance(eventPriceCategory.EventId);
            if (events == null)
            {
                context.AddFailure("EventId", "The event doesnot exist. ");
                return;
            }

            var list  = await _appDbContext.EventTicketCategories.Where(x => x.EventId == eventPriceCategory.EventId).ToListAsync();
            int existingTotalCapacity = list.Sum(x => x.Capacity);
            
            

            var venueid = events.VenueId ;
            var venue = await venueRepository.GetVenue(venueid);
            int updatingCapacity = 0;
            if (eventPriceCategory.Id != null)
            {
                //in case of updation
                var existingCategory = _appDbContext.EventTicketCategories.FirstOrDefault(x => x.Id == eventPriceCategory.Id);
                updatingCapacity = existingCategory.Capacity;
                existingTotalCapacity = existingTotalCapacity - updatingCapacity;
            }
            int remainingCapacity = venue.MaxCapacity - existingTotalCapacity;
            if (remainingCapacity < eventPriceCategory.Capacity)
            {
                remainingCapacity -= updatingCapacity;
                context.AddFailure($"The total capacity of venue exceed. Remaing seats for adding Category is {remainingCapacity}");
            }
            
        }
    }
}
