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
       
        public OfferValidator()
        {

            RuleFor(x => x.Type)
                .IsInEnum()
            .WithMessage("Invalid Offer Type. Allowed values are 0 (EarlyBird) and 1 (Group).");
            
            RuleFor(o => o.Discount)
                .GreaterThan(0)
                .LessThan(100)
                .WithMessage("Invalid Offer Discount. Range Must be from 0.00 to 99.99");
            RuleFor(o => o)
                 .Must(ValidateTypeParameters)
                 .WithMessage("Invalid parameters for the offer type.");

        }     

        private bool ValidateTypeParameters(OfferDTO offer)
        {
            if (offer.Type == (int)OfferTypeEnum.EarlyBird)
            {
                return offer.GroupSize == 0;
            }
            else
            {
                return offer.Quantity == 0;
            }
        }  

    }
}
