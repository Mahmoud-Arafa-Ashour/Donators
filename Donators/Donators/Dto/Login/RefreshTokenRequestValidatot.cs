namespace Donators.Contracts.login
{
    public class RefreshTokenRequestValidatot : AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenRequestValidatot()
        {
            RuleFor(x => x.Token).NotEmpty();
            RuleFor(x => x.RefreshToken).NotEmpty();
        }
    }
}
