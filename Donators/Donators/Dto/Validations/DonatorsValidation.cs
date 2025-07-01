using Donators.Entites;
using FluentValidation;

namespace Donators.Dto.Validations;

public class DonatorsValidation : AbstractValidator<DonatorDto>
{
    public DonatorsValidation()
    {
        RuleFor(_ => _.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(500)
            .WithMessage("Name must not exceed 500 characters.");
        RuleFor(_ => _.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.")
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .WithMessage("Phone number must be a valid international format.");
    }
}
