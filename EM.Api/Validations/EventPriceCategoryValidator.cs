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
        public EventPriceCategoryValidator()
        {
            RuleFor(n => n.Name)
                .NotNull().WithMessage("Please provide a valid name.")
                .Length(1, 50).WithMessage("Name length limit exceeded");

            RuleFor(x => x.Price)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0.");
            RuleFor(o => o.Capacity)
                .GreaterThan(0).WithMessage("Capacity must be greater than 0");
        }
    }
}
