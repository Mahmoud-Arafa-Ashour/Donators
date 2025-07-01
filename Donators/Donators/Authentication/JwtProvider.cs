using Donators.Entites;

namespace Donators.Authentication;

public class JwtProvider : IJwtProvider
{
    public (string token, int expirein) GenerateToken(ApplicationUser user)
    {
        throw new NotImplementedException();
    }

    public string? ValidateToken(string token)
    {
        throw new NotImplementedException();
    }
}
