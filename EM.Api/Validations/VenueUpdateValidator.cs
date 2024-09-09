using EM.Core.DTOs.Request;
using FluentValidation;

namespace EM.Api.Validations
{
    public class VenueUpdateValidator : AbstractValidator<VenueUpdateDTO>
    {
        public VenueUpdateValidator() {

            RuleFor(venue => venue.Name)
                 .NotEmpty().WithMessage("Name is Required").Length(1, 100).WithMessage("limit exceeded");

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

            RuleFor(venue => venue.Description)
                .Length(1, 250).WithMessage("Please describe in less than 250 character");


        }
    }
}
