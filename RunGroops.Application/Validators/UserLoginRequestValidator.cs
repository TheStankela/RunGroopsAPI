using FluentValidation;
using FluentValidation.Validators;
using RunGroops.Application.Models;

namespace RunGroops.Application.Validators
{
    public class UserLoginRequestValidator : AbstractValidator<UserLoginRequest>
    {
        public UserLoginRequestValidator() 
        {
            RuleFor(x => x.Email)
                .NotNull()
                .WithMessage("Email cannot be null.")
                .NotEmpty()
                .WithMessage("Email cannot be empty.")
                .EmailAddress()
                .WithMessage("Invalid email address.");

            RuleFor(x => x.Password)
                .NotNull()
                .WithMessage("Password cannot be null.")
                .NotEmpty()
                .WithMessage("Password cannot be empty.")
                .MinimumLength(8)
                .WithMessage("Password must be atleast 8 characters long.")
                .MaximumLength(24)
                .WithMessage("Password cannot be longer than 24 characters.");
        }

    }
}
