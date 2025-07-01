namespace Donators.Services;

public class MessageService(DonatorsDBContext dBContext) : IMessageService
{
    private readonly DonatorsDBContext _dBContext = dBContext;
    public async Task<Result<MessageDto>> Add(MessageDto model, CancellationToken cancellationToken = default)
    {
        if (model is null || string.IsNullOrWhiteSpace(model.Content))
        {
            return Result.Failure<MessageDto>(MessageErrors.NullMessage);
        }
        var result = model.Adapt<Message>();
        _dBContext.Messages.Add(result);
        await _dBContext.SaveChangesAsync(cancellationToken);
        return Result.Success(result.Adapt<MessageDto>());
    }

    public async Task<Result> Delete(int id, CancellationToken cancellationToken = default)
    {
        var isExixted = await _dBContext.Messages.FirstOrDefaultAsync(x => x.Id == id , cancellationToken);
        if (isExixted is null)
        {
            return Result.Failure(MessageErrors.EmptyMessage);
        }
        _dBContext.Messages.Remove(isExixted);
        await _dBContext.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result<IEnumerable<MessageDto>>> GetAll(CancellationToken cancellationToken = default)
    {
        var messages = await _dBContext.Messages
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        return Result.Success(messages.Adapt<IEnumerable<MessageDto>>());
    }

    public async Task<Result<MessageDto>> GetById(int id, CancellationToken cancellationToken = default)
    {
        var message = await _dBContext.Messages
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (message is null)
            return Result.Failure<MessageDto>(MessageErrors.EmptyMessage);
        return Result.Success(message.Adapt<MessageDto>());
    }

    public async Task<Result> Update(int Id , MessageDto model, CancellationToken cancellationToken = default)
    {
        var message = await _dBContext.Messages
            .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        if (message is null)
            return Result.Failure(MessageErrors.EmptyMessage);
        if (string.IsNullOrWhiteSpace(model.Content))
            return Result.Failure(MessageErrors.NullMessage);
        message.Content = model.Content;
        _dBContext.Messages.Update(message);
        await _dBContext.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
