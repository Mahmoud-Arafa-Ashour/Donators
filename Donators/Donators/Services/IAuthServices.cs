using Donators.Contracts.Auth;

namespace Donators.Services;

public interface IAuthServices
{
    Task<Result<AuthResponse>> GetTokenAsync(string email , string password , CancellationToken cancellationToken = default);
    Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken);
    Task<Result> RevokeRefreshToken(string Token, string refreshToken, CancellationToken cancellationToken);
    Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);
}
