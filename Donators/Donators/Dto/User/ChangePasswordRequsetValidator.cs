namespace Donators.Contracts.User
{
    public class ChangePasswordRequsetValidator : AbstractValidator<ChangePasswordRequset>
    {
        public ChangePasswordRequsetValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty();
            RuleFor(x => x.NewPassword)
               .NotEmpty()
               .Matches(PasswordPatterns.PasswordRegix)
               .WithMessage("Password should be at least 8 digits and should contains Lowercase, NonAlphanumeric and Uppercase")
               .NotEqual(x=>x.CurrentPassword)
               .WithMessage("New Passord Can not be similar to the old one");
        }
    }
}
