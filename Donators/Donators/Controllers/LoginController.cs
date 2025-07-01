using Donators.Services;

namespace Donators.Controllers;

[Route("api/[controller]/[Action]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IAuthServices _authServices;
    private readonly ILogger<LoginController> _logger;

    public LoginController(IAuthServices authServices, ILogger<LoginController> logger)
    {
        _authServices = authServices;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody]LoginRequest request, CancellationToken cancellationToken)
    {
        var authResult = await _authServices.GetTokenAsync(request.Email, request.Password, cancellationToken);
        return authResult.IsSuccess ? Ok(authResult.Value) : authResult.ToProblem();
    }
    [HttpPost()]
    public async Task<IActionResult> RefreshToken([FromBody]RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var authResponse = await _authServices.GetRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);
        return authResponse.IsSuccess ?
           Ok() :
           authResponse.ToProblem();
    }
    [HttpPost()]
    public async Task<IActionResult> RevokeRefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var authResult = await _authServices.RevokeRefreshToken(request.Token, request.RefreshToken, cancellationToken);
        return authResult.IsSuccess ?
            Ok() :
            authResult.ToProblem();
    }
    [HttpPost()]
    public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
    {
        var result = await _authServices.RegisterAsync(request, cancellationToken);
        return result.IsSuccess ?
            Ok() :
            result.ToProblem();
    }
}
