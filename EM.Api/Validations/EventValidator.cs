using EM.Business.Services;
using EM.Core.DTOs.Request;
using EM.Data.Entities;
using EM.Data.Repositories;
using EM.Data.Repository;
using FluentValidation;

namespace EM.Api.Validations
{
    public class EventValidator : AbstractValidator<EventDTO>
    {
        IVenueRepository _venueRepository;
        IPerformerRepository _performerRepository;
        public EventValidator(IPerformerRepository performerRepo, IVenueRepository venueRepo)
        {
            _venueRepository = venueRepo;
            _performerRepository = performerRepo;
            RuleFor(e => e.Title)
                .NotEmpty().WithMessage("Event Title is Required")
                .MinimumLength(10).WithMessage("Title must be longer than 10 characters")
                .MaximumLength(100).WithMessage("Title must be under 100 characters");

            RuleFor(e => e.Description)
                .NotEmpty().WithMessage("Event Description is Required")
                .MinimumLength(10).WithMessage("Event Description must be longer than 10 characters")
                .MaximumLength(300).WithMessage("Event Description must be under 300 characters");

            RuleFor(e => e.Price)
                .NotEmpty().WithMessage("Event Base price is Required")
                .GreaterThan(0).WithMessage("Event Base price should be greater than 0");

            RuleFor(e => e.PerformerId)
                .NotEmpty().WithMessage("Performer Id is Required")
                .GreaterThan(0).WithMessage("Performer Id must be greater than 0.")
                .MustAsync(PerformerExists).WithMessage("The provided Performer Id does not exist.");

            RuleFor(e => e.VenueId)
                .NotEmpty().WithMessage("Venue Id is Required")
                .GreaterThan(0).WithMessage("Venue Id must be greater than 0.")
                .MustAsync(VenueExists).WithMessage("The provided Venue Id does not exist.");

            RuleFor(e => e.StartDateTime)
                .NotEmpty().WithMessage("Start Date and Time is required");

            RuleFor(e => e.EndDateTime)
                .NotEmpty().WithMessage("End Date and Time is Required");

            RuleFor(e => e)
                .Must(ValidateDateRange).WithMessage("End Date Must be greater than Start Date");

            RuleFor(e => e)
                 .Must(ValidateDate).WithMessage("Invalid Date Format");

            RuleFor(e => e)
                .Must(ValidateStartDate).WithMessage("Can't Schedule Event on Same day.");
        }

        private bool ValidateDate(EventDTO e)
        {
            DateTime startDateTime;
            bool isValidStartTime = DateTime.TryParseExact(e.StartDateTime, "yyyy-MM-ddTHH:mm:ssZ", null, System.Globalization.DateTimeStyles.None, out startDateTime);
            DateTime endDateTime;
            bool isValidEndTime = DateTime.TryParseExact(e.EndDateTime, "yyyy-MM-ddTHH:mm:ssZ", null, System.Globalization.DateTimeStyles.None, out endDateTime);
            if (isValidStartTime && isValidEndTime)
            {
                return true;
            }
            else return false;
        }

        private bool ValidateStartDate(EventDTO e)
        {
            DateTime startDateTime;
            bool isValidStartTime = DateTime.TryParseExact(e.StartDateTime, "yyyy-MM-ddTHH:mm:ssZ", null, System.Globalization.DateTimeStyles.None, out startDateTime);
            return startDateTime > DateTime.UtcNow.AddDays(1);
        }

        private bool ValidateDateRange(EventDTO e)
        {
            DateTime startDateTime;
            bool isValidStartTime = DateTime.TryParseExact(e.StartDateTime, "yyyy-MM-ddTHH:mm:ssZ", null, System.Globalization.DateTimeStyles.None, out startDateTime);
            DateTime endDateTime;
            bool isValidEndTime = DateTime.TryParseExact(e.EndDateTime, "yyyy-MM-ddTHH:mm:ssZ", null, System.Globalization.DateTimeStyles.None, out endDateTime);
            if (isValidStartTime && isValidEndTime)
            {
                if(startDateTime.Date == endDateTime.Date)
                {
                    return startDateTime.TimeOfDay < endDateTime.TimeOfDay;
                }
                return startDateTime < endDateTime;
            }
            else return false;
        }

        private async Task<bool> PerformerExists(int performerId, CancellationToken token)
        {
            return await _performerRepository.PerformerExistsAsync(performerId);
        }

        private async Task<bool> VenueExists(int venueId, CancellationToken token)
        {
            return await _venueRepository.VenueExistsAsync(venueId);
        }
    }
}
