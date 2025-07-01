namespace Donators.Controllers;

[Route("api/[controller]/[Action]")]
[ApiController]
public class MessageController(IMessageService messageService) : ControllerBase
{
    private readonly IMessageService _messageService = messageService;
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var result = await _messageService.GetAll(cancellationToken);   
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
    {
        var result = await _messageService.GetById(id, cancellationToken);
        return result.IsFailure ? result.ToProblem() : Ok();
    }
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] MessageDto model, CancellationToken cancellationToken = default)
    {
        var result = await _messageService.Add(model, cancellationToken);
        return result.IsFailure ? result.ToProblem() : Ok(result.Value);
    }
    [HttpPut]
    public async Task<IActionResult> Update(int id, [FromBody] MessageDto model, CancellationToken cancellationToken = default)
    {
        var result = await _messageService.Update(id, model, cancellationToken);
        return result.IsFailure ? result.ToProblem() : Ok();
    }
    [HttpDelete]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
    {
        var result = await _messageService.Delete(id, cancellationToken);
        return result.IsFailure ? result.ToProblem() : Ok();
    }
}
