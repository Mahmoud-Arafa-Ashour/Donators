using Microsoft.AspNetCore.Identity;

namespace Donators.Entites
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string CharityName { get; set; } = string.Empty;
    }
}
