using EM.Core.DTOs.Request;
using EM.Data.Entities;
using EM.Data.Repository;
using FluentValidation;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace EM.Api.Validations
{
    public class VenueRequestValidator : AbstractValidator<VenueRequestDTO>
    {
        private readonly IVenueRepository _venueRepository;
        public VenueRequestValidator(IVenueRepository venueRepository)
        {
            _venueRepository = venueRepository;
            RuleFor(venue => venue.Name)
                .NotEmpty().WithMessage("Name is Required").Length(1,100).WithMessage("limit exceeded")
                .MustAsync(VenueNotExist).WithMessage("Venue Already Exist");

            RuleFor(venue => venue.Type)
                .IsInEnum().WithMessage("Select Valid Venue Type");

            RuleFor(venue => venue.MaxCapacity)
                .NotEmpty().WithMessage("Max Capacity is required").GreaterThan(0).WithMessage("MaxCapacity must be greater than 0."); ;

            RuleFor(venue => venue.AddressLine1)
                .NotEmpty().WithMessage("Address is Required").Length(1, 100).WithMessage("limit exceeded");

            
            RuleFor(venue => venue.ZipCode)
                .NotEmpty().WithMessage("ZipCode is Required").Must(zipCode => zipCode.ToString().Length == 6)
                .WithMessage("ZipCode must be exactly 6 digits long.");

            RuleFor(venue => venue.City)
                .NotEmpty().WithMessage("City is Required");

            RuleFor(venue => venue.State)
                .NotEmpty().WithMessage("State is Required");

            RuleFor(venue => venue.CountryId)
                .NotEmpty().WithMessage("Country is Required");

            RuleFor(venue => venue.Description)
                .NotEmpty().WithMessage("Description is required")
                .Length(1, 250).WithMessage("Please describe in less than 250 character");
        }

        private async Task<bool> VenueNotExist(string venueName, CancellationToken cancellationToken)
        {
            var result = await _venueRepository.VenueNameExistsAsync(venueName);
            return !result;
        }
    }
}

 
