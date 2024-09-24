using EM.Core.DTOs.Request;
using EM.Data.Repositories;
using FluentValidation;

namespace EM.Api.Validations
{
    public class PerformerValidator : AbstractValidator<PerformerDTO>
    {
        private readonly IPerformerRepository _performerRepository;

        public PerformerValidator(IPerformerRepository performerRepository)
        {
            _performerRepository = performerRepository;

            RuleFor(performer => performer.Name)
                .NotEmpty().WithMessage("Performer Name is required")
                .MustAsync(PerformerNotExist).WithMessage("Performer Already Exists");

            RuleFor(performer => performer.Base64String)
                .NotEmpty().WithMessage("Image is required");

            RuleFor(performer => performer.Bio)
                .NotEmpty().WithMessage("Performer Bio is required");
        }

        private async Task<bool> PerformerNotExist(string performerName, CancellationToken cancellationToken)
        {
            var result = await _performerRepository.PerformerNameExistAsync(performerName);
            return !result;
        }
    }
}
