namespace Donators.Contracts.Auth;

public class ConfirmEmailRequestValidator : AbstractValidator<ConfirmEmailRequest>
{
    public ConfirmEmailRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required.");
        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("Code is required.");
    }
}
