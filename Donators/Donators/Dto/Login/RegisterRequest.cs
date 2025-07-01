namespace Donators.Contracts.login
{
    public record RegisterRequest([EmailAddress] string Email , string Password , string Name , [Length(11,100)]string PhoneNumber , string Address , string CharityName);
}
