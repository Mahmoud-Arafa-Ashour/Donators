namespace Donators.Contracts.login
{
    public record RefreshTokenRequest
        (
        string Token , 
        string RefreshToken
        );
}
