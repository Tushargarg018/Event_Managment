using FluentValidation;
using EM.Core.DTOs.Request;
using EM.Data.Repositories;

namespace EM.Api.Validations
{
    public class StateIdValidator : AbstractValidator<int>
	{
		private readonly IStateRepository _stateRepository;
		public StateIdValidator(IStateRepository stateRepository)
		{
			_stateRepository = stateRepository;
			RuleFor(stateId => stateId)
			.GreaterThan(0).WithMessage("State ID must be greater than 0.")
			.MustAsync(StateExists).WithMessage("The provided State ID does not exist.");

		}
		private async Task<bool> StateExists(int stateId, CancellationToken cancellationToken)
		{
			return await _stateRepository.StateExistsAsync(stateId);
		}
	}
}
