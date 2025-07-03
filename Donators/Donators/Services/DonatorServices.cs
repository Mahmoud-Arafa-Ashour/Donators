

using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Donators.Services;

public class DonatorServices(DonatorsDBContext donatorsDB) : IDonatorServices
{
    private readonly DonatorsDBContext _donatorsDB = donatorsDB;

    public async Task<Result<DonatorDto>> Add(DonatorDto DonatorDto, CancellationToken cancellationToken)
    {
        if(DonatorDto is null || string.IsNullOrWhiteSpace(DonatorDto.Name) || string.IsNullOrWhiteSpace(DonatorDto.PhoneNumber))
            return Result.Failure<DonatorDto>(DonatorErrors.Null);
        var result = DonatorDto.Adapt<Donator>();
        _donatorsDB.Donators.Add(result);
        await _donatorsDB.SaveChangesAsync(cancellationToken);
        return Result.Success(result.Adapt<DonatorDto>());
    }
    public async Task<Result<List<DonatorDto>>> AddList(List<DonatorDto> models, CancellationToken cancellationToken)
    {
        foreach (var model in models)
        {
            if (model is null || string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.PhoneNumber))
                return Result.Failure<List<DonatorDto>>(DonatorErrors.Null);
        }
        var result = models.Adapt<List<Donator>>();
        await _donatorsDB.AddRangeAsync(result , cancellationToken);
        await _donatorsDB.SaveChangesAsync(cancellationToken);
        return Result.Success(result.Adapt<List<DonatorDto>>());
    }
    public Task<Result> BulkWhatsAppMessages(List<DonatorDto> DonatorDtos, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _donatorsDB.Donators.FirstOrDefaultAsync(_ => _.Id == id, cancellationToken);
        if (result == null)
            return Result.Failure<DonatorDto>(DonatorErrors.NotFound);
        try 
        {
            _donatorsDB.Donators.Remove(result);
            await _donatorsDB.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex) 
        {
            return Result.Failure(new Error("Faild Deletion" , ex.Message , StatusCodes.Status409Conflict));
        }
    }

    public async Task<Result<List<DonatorDto>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _donatorsDB.Donators
            .ProjectToType<DonatorDto>()
            .ToListAsync(cancellationToken);

        return Result.Success(result);
    }

    public async Task<Result<DonatorDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _donatorsDB.Donators.FirstOrDefaultAsync(_ => _.Id == id , cancellationToken);
        if(result == null) 
            return Result.Failure<DonatorDto>(DonatorErrors.NotFound);
        return Result.Success(result.Adapt<DonatorDto>());
    }

    public async Task<Result<DonatorDto>> GetByName(string name, CancellationToken cancellationToken)
    {
        var result = await _donatorsDB.Donators.FirstOrDefaultAsync(_ => _.Name == name, cancellationToken);
        if (result == null)
            return Result.Failure<DonatorDto>(DonatorErrors.NotFound);
        return Result.Success(result.Adapt<DonatorDto>());
    }

    public async Task<Result<DonatorDto>> GetByPhone(string phone, CancellationToken cancellationToken)
    {
        var result = await _donatorsDB.Donators.FirstOrDefaultAsync(_ => _.PhoneNumber == phone, cancellationToken);
        if (result == null)
            return Result.Failure<DonatorDto>(DonatorErrors.NotFound);
        return Result.Success(result.Adapt<DonatorDto>());
    }

    public async Task<Result> ReadFromExcel(IFormFile file, CancellationToken cancellationToken)
    {
        if(file == null)
            return Result.Failure(ExcelErrors.Null);
        if (Path.GetExtension(file.FileName).ToLower() != ".xlsx")
            return Result.Failure(ExcelErrors.NotMatch);
        using var stream = file.OpenReadStream();
        var readResult = await ExcelReader.ReadDonatorExcelAsync(stream);
        if (readResult.IsFailure)
            return Result.Failure(new Error(readResult.Error.code, readResult.Error.description, StatusCodes.Status409Conflict));
        await AddList(readResult.Value, cancellationToken);
        return Result.Success();
    }

    public Task<Result> SendWhatsAppMessage(string phone, string message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> Update(int id, DonatorDto DonatorDto, CancellationToken cancellationToken)
    {
        if (DonatorDto is null || string.IsNullOrWhiteSpace(DonatorDto.Name) || string.IsNullOrWhiteSpace(DonatorDto.PhoneNumber))
            return Result.Failure(DonatorErrors.Null);
        var result = await _donatorsDB.Donators.FirstOrDefaultAsync(_ => _.Id == id, cancellationToken);
        if (result == null)
            return Result.Failure<DonatorDto>(DonatorErrors.NotFound);
        result.PhoneNumber = DonatorDto.PhoneNumber;
        result.Name = DonatorDto.Name;
        await _donatorsDB.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
