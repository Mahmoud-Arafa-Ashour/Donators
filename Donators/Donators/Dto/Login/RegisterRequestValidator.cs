namespace Donators.Contracts.login
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email).
                EmailAddress().
                NotEmpty();
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.Address)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.Password)
                .NotEmpty()
                .Matches(PasswordPatterns.PasswordRegix)
                .WithMessage("Password should be at least 8 digits and should contains Lowercase, NonAlphanumeric and Uppercase");
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .MinimumLength(11);
            RuleFor(x => x.ResturantName)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
    public static class PasswordPatterns
    {
        public const string PasswordRegix = "(?=(.*[0-9]))(?=.*[\\!@#$%^&*()\\\\[\\]{}\\-_+=~`|:;\"'<>,./?])(?=.*[a-z])(?=(.*[A-Z]))(?=(.*)).{8,}";
    }
}
