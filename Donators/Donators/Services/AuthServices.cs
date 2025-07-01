using Donators.Authentication;
using Donators.Contracts.Auth;
using Donators.Entites;
using System.Security.Cryptography;
namespace Donators.Services
{
    public class AuthServices(UserManager<ApplicationUser> userManager 
        , IJwtProvider jwtProvidor 
        , ILogger<ApplicationUser> logger
        ,SignInManager<ApplicationUser> signInManager
        , IHttpContextAccessor httpContextAccessor) 
        : IAuthServices
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IJwtProvider _jwtProvidor = jwtProvidor;
        private readonly ILogger<ApplicationUser> _logger = logger;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly int _refreshTokenExpiration = 14;

        public async Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            //check user existance 
            if (await _userManager.FindByEmailAsync(email) is not { } user)
                return Result.Failure<AuthResponse>(UserErrors.InvalidCredentionals);
            //check password 
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded)
            {
                //generate token
                var (token, expiresin) = _jwtProvidor.GenerateToken(user);
                //generate Refresh Token
                var refreshToken = GenerateRefreshToken();
                var refreshTokenExpirationDate = DateTime.UtcNow.AddDays(_refreshTokenExpiration);
                //save refresh token to database 
                user.RefreshTokens.Add(new RefreshTokens
                {
                    Token = refreshToken,
                    ExpiresOn = refreshTokenExpirationDate
                });
                await _userManager.UpdateAsync(user);
                var response = new AuthResponse(user.Id, user.Email!, $"{user.FirstName} {user.LastName}", user.Adress, user.CharityName, user.PhoneNumber!, token, expiresin, refreshToken, refreshTokenExpirationDate);
                return Result.Success(response);
            }
            return Result.Failure<AuthResponse>(result.IsNotAllowed ? UserErrors.EmailNotConfirmed : UserErrors.InvalidCredentionals);
        }
        public async Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken)
        {
            var userId = jwtProvidor.ValidateToken(token);
            if (userId is null) return Result.Failure<AuthResponse>(TokenErrors.EmptyToken);
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return Result.Failure<AuthResponse>(TokenErrors.EmptyToken);
            var UserrefreshToken = user.RefreshTokens?.FirstOrDefault(x => x.Token == refreshToken && x.IsActive);
            if (UserrefreshToken == null) return Result.Failure<AuthResponse>(TokenErrors.EmptyToken);
            UserrefreshToken.RevokedOn = DateTime.UtcNow;
            var (newtoken, expiresin) = _jwtProvidor.GenerateToken(user);
            var newrefreshToken = GenerateRefreshToken();
            var refreshTokenExpirationDate = DateTime.UtcNow.AddDays(_refreshTokenExpiration);
            user.RefreshTokens!.Add(new RefreshTokens
            {
                Token = newrefreshToken,
                ExpiresOn = refreshTokenExpirationDate
            });
            await _userManager.UpdateAsync(user);
            var response = new AuthResponse(user.Id, user.Email!, $"{user.FirstName} {user.LastName}", user.Adress, user.CharityName, user.PhoneNumber!, newtoken, expiresin, newrefreshToken, refreshTokenExpirationDate);
            return Result.Success(response);
        }
        public async Task<Result> RevokeRefreshToken(string Token, string refreshToken, CancellationToken cancellationToken)
        {
            //check for the valid token 
            var userId = jwtProvidor.ValidateToken(Token);
            if (userId is null) return Result.Failure(TokenErrors.EmptyToken);
            //check for the id
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return Result.Failure(TokenErrors.EmptyToken);
            //check for the refresh token 
            var UserrefreshToken = user.RefreshTokens?.FirstOrDefault(x => x.Token == refreshToken && x.IsActive);
            if (UserrefreshToken == null) return Result.Failure(TokenErrors.EmptyToken);
            //make it valid for one time 
            UserrefreshToken.RevokedOn = DateTime.UtcNow;
            await userManager.UpdateAsync(user);
            return Result.Success();
        }
        public async Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
        {
            //check if the email is Duplicated
            var IsExistedEmail = await _userManager.Users.AnyAsync(x => x.Email == request.Email);
            if (IsExistedEmail)
                return Result.Failure(UserErrors.DuplicateEmail);
            var user = request.Adapt<ApplicationUser>();
            user.UserName = request.Email;
            user.Adress = request.Address;
            user.CharityName = request.CharityName;
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                //generate token
                var (token, expiresin) = _jwtProvidor.GenerateToken(user);
                //generate Refresh Token
                var refreshToken = GenerateRefreshToken();
                var refreshTokenExpirationDate = DateTime.UtcNow.AddDays(_refreshTokenExpiration);
                //save refresh token to database 
                user.RefreshTokens.Add(new RefreshTokens
                {
                    Token = refreshToken,
                    ExpiresOn = refreshTokenExpirationDate
                });
                await _userManager.UpdateAsync(user);
                var response = new AuthResponse(user.Id, user.Email!, $"{user.FirstName} {user.LastName}", user.Adress, user.CharityName, user.PhoneNumber!, token, expiresin, refreshToken, refreshTokenExpirationDate);
                return Result.Success(response);
            }
            var error = result.Errors.First();
            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }
        private static string GenerateRefreshToken() =>
             Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}
