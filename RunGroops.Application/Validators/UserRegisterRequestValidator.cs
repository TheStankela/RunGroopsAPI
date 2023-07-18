using FluentValidation;
using RunGroops.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunGroops.Application.Validators
{
    public class UserRegisterRequestValidator : AbstractValidator<UserRegisterRequest>
    {
        public UserRegisterRequestValidator()
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
