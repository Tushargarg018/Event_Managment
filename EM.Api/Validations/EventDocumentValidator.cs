using EM.Core.DTOs.Request;
using EM.Core.DTOs.Response;
using EM.Data.Entities;
using EM.Data.Migrations;
using FluentValidation;

namespace EM.Api.Validations
{
    public class EventDocumentValidator : AbstractValidator<EventDocumentRequestDTO>
    {
        public EventDocumentValidator()
        {
            RuleFor(doc => doc.Title)
                 .NotEmpty().WithMessage("Title is Required").Length(1, 250).WithMessage("title length exceeded");

            RuleFor(doc => doc.Base64String)
                 .Must(checkExtension).WithMessage("Only .jpg, .png and .jpeg allowed");

            RuleFor(doc => doc.GetType())
                .NotEmpty().WithMessage("Document Type is required");

        }

        private bool checkExtension(string base64String)
        {
            string[] allowedExtensions = ["jpg", "png", "jpeg"];
            var header = base64String.Split(',')[0];
            int startIndex = header.IndexOf('/') + 1;
            int endIndex = header.IndexOf(';');
            var extension = header.Substring(startIndex, endIndex - startIndex);
            bool result = Array.Exists(allowedExtensions, element => element == extension);
            return result;
        }
    }
}
