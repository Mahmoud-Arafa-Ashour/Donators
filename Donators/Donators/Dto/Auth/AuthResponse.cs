namespace Donators.Contracts.Auth;

public record AuthResponse
    (string id ,
    string email ,
    string Name ,
    string Adress ,
    string CharityName ,
    string PhoneNumber,
    string token ,
    int expiresin,
    string RefreshToken,
    DateTime RefeshTokenExpiration
    );
