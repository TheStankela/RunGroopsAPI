using FluentValidation;
using RunGroops.Application.Models;

namespace RunGroops.Application.Validators
{
    public class ClubRequestValidator : AbstractValidator<ClubRequest>
    {
        public ClubRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().NotEmpty();

            RuleFor(x => x.Description)
                .NotNull().NotEmpty();

            RuleFor(x => x.ClubCategory)
                .NotNull().NotEmpty();
        }
    }
}
