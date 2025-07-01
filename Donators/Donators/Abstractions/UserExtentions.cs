using System.Security.Claims;

namespace Donators.Abstractions;

public static class UserExtentions
{
    public static string? GetUserID(this ClaimsPrincipal user) =>
        user.FindFirstValue(ClaimTypes.NameIdentifier);
}
