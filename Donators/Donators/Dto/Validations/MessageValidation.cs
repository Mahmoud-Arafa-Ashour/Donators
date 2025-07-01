using Donators.Entites;
using FluentValidation;

namespace Donators.Dto.Validations;

public class MessageValidation : AbstractValidator<MessageDto>
{
    public MessageValidation()
    {
        RuleFor(_ => _.Content)
            .NotEmpty()
            .WithMessage("Content is required.")
            .MaximumLength(500)
            .WithMessage("Content must not exceed 500 characters.");
    }
}
