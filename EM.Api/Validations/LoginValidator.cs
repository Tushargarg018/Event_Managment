using EM.Core.DTOs.Request;
using FluentValidation;

namespace EM.Api.Validations
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is Required");
            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password is Required");
        }
    }
}
