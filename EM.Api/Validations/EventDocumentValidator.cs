using EM.Core.DTOs.Request;
using EM.Core.DTOs.Response;
using EM.Data.Entities;
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
                 .NotEmpty().WithMessage("document is Required").Must(file => file.Length <= 1 * 1024 * 1024).WithMessage("Image size must be less than or equal to 1MB");
        }
    }
}
