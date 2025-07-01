namespace Donators.Services;

public interface IMessageService
{
    Task<Result<MessageDto>> Add(MessageDto model, CancellationToken cancellationToken = default);
    Task<Result> Update(int Id , MessageDto model, CancellationToken cancellationToken = default);
    Task<Result> Delete(int id, CancellationToken cancellationToken = default);
    Task<Result<MessageDto>> GetById(int id, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<MessageDto>>> GetAll(CancellationToken cancellationToken = default);
}
