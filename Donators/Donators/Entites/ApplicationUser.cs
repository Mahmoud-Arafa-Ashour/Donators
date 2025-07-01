using Donators.Models;
namespace Donators.Entites;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string CharityName { get; set; } = string.Empty;
    public string Adress { get; set; } = string.Empty;
    public List<RefreshTokens> RefreshTokens { get; set; } = [];
}
