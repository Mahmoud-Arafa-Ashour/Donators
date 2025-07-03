namespace Donators.Services;

public interface IDonatorServices
{
    Task<Result<DonatorDto>> Add(DonatorDto DonatorDto , CancellationToken cancellationToken);
    Task<Result<List<DonatorDto>>> AddList(List<DonatorDto> models, CancellationToken cancellationToken);
    Task<Result> Update(int id , DonatorDto DonatorDto, CancellationToken cancellationToken);
    Task<Result> Delete(int id, CancellationToken cancellationToken);
    Task<Result<DonatorDto>> GetById(int id, CancellationToken cancellationToken);
    Task<Result<List<DonatorDto>>> GetAll(CancellationToken cancellationToken);
    Task<Result<DonatorDto>> GetByName(string name, CancellationToken cancellationToken);
    Task<Result<DonatorDto>> GetByPhone(string phone, CancellationToken cancellationToken);
    Task<Result> BulkWhatsAppMessages(List<DonatorDto> DonatorDtos, CancellationToken cancellationToken);
    Task<Result> ReadFromExcel(IFormFile file, CancellationToken cancellationToken);
    Task<Result> SendWhatsAppMessage(string phone, string message, CancellationToken cancellationToken);

}
