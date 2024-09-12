using EM.Core.DTOs.Request;
using FluentValidation;

namespace EM.Api.Validations
{
    public class EventDocumentValidator : AbstractValidator<EventDocumentRequestDTO>
    {
        public EventDocumentValidator()
        {
            RuleFor(doc => doc.Title)
                 .NotEmpty().WithMessage("Title is Required").Length(1, 20).WithMessage("title length exceeded");

            RuleFor(doc => doc.ImageFile)
                 .NotEmpty().WithMessage("document is Required");
        }
    }
}
