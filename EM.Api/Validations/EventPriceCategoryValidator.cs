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
        private readonly IEventPriceCategoryRepository _eventPriceCategoryRepository;
        private readonly IVenueRepository _venueRepository;
        private readonly IEventRepository _eventRepository;
        private readonly AppDbContext _appDbContext;

        public EventPriceCategoryValidator(IEventPriceCategoryRepository eventPriceCategoryRepository, IVenueRepository venueRepository, IEventRepository eventRepository)
        {
            _eventRepository  = eventRepository;
            _eventPriceCategoryRepository = eventPriceCategoryRepository;
            _venueRepository = venueRepository;

            RuleFor(n => n.Name)
                .NotNull().WithMessage("Please provide a valid name.")
                .Length(1, 50).WithMessage("Name length limit exceeded");

            

            RuleFor(x => x.Price)
                .NotNull()
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0.");

            RuleFor(o => o.EventId)
                .MustAsync(EventExistsAsync).WithMessage("Event Does not exist")
                .MustAsync(EventNotPublished).WithMessage("Event is Already Published");

            RuleFor(o => o.Capacity)
            .GreaterThan(0).WithMessage("Capacity must be greater than 0")
            .MustAsync(async (o, capacity, cancellation) => await IsValidCapacity(o))
            .WithMessage("Seat category capacity exceeds available capacity for the event");
        }
        private async Task<bool> EventExistsAsync(int eventId, CancellationToken token)
        {
            return await _eventRepository.EventExistsAsync(eventId);
        }
        private async Task<bool> EventNotPublished(int eventId, CancellationToken token)
        {
            return await _eventRepository.EventNotPublished(eventId);
        }

        private async Task<bool> IsValidCapacity(EventPriceCategoryRequestDTO eventPriceCategoryDTO)
        {
            var eventDetails = await _eventRepository.GetEventByIdAsync(eventPriceCategoryDTO.EventId);
            if (eventDetails == null)
            {
                return false;
            }
            var venueCapacity = await _venueRepository.GetVenueCapacityByIdAsync(eventDetails.VenueId);

            var totalAllocatedCapacity = await _eventPriceCategoryRepository.GetTotalAllocatedSeatCapacityAsync(eventPriceCategoryDTO.EventId);

            if (eventPriceCategoryDTO.Id != null)
            {
                var existingCategoryCapacity = await _eventPriceCategoryRepository.GetCategoryCapacityByIdAsync(eventPriceCategoryDTO.Id);
                totalAllocatedCapacity -= existingCategoryCapacity;
            }

            return totalAllocatedCapacity + eventPriceCategoryDTO.Capacity <= venueCapacity;
        }
    }
}
