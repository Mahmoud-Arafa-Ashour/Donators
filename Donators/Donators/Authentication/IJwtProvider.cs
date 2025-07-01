using Donators.Entites;

namespace Donators.Authentication;

public interface IJwtProvider
{
    (string token, int expirein) GenerateToken(ApplicationUser user);
    string? ValidateToken(string token);
}
