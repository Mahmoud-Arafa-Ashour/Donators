using OfficeOpenXml;
namespace Donators.Services;
public static class ExcelReader
{
    public static async Task<Result<List<DonatorDto>>> ReadDonatorExcelAsync(Stream excelStream)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using var package = new ExcelPackage(excelStream);
        var worksheet = package.Workbook.Worksheets.FirstOrDefault();

        if (worksheet == null || worksheet.Dimension == null)
        {
            return Result.Failure<List<DonatorDto>>(DonatorErrors.Null);
        }

        var properties = typeof(DonatorDto).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var propertyDict = properties.ToDictionary(p => p.Name.ToLower(), p => p);

        int rowCount = worksheet.Dimension.End.Row;
        int colCount = worksheet.Dimension.End.Column;

        // Get header index mapping
        var headerIndexMap = new Dictionary<string, int>();
        for (int col = 1; col <= colCount; col++)
        {
            var header = worksheet.Cells[1, col].Text.Trim().ToLower();
            if (!string.IsNullOrWhiteSpace(header) && !headerIndexMap.ContainsKey(header))
                headerIndexMap[header] = col;
        }

        var resultList = new List<DonatorDto>();

        for (int row = 2; row <= rowCount; row++)
        {
            var dto = new DonatorDto();

            foreach (var prop in properties)
            {
                if (headerIndexMap.TryGetValue(prop.Name.ToLower(), out int col))
                {
                    var cell = worksheet.Cells[row, col];
                    if (cell.Value != null)
                    {
                        try
                        {
                            object convertedValue = Convert.ChangeType(cell.Value, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                            prop.SetValue(dto, convertedValue);
                        }
                        catch
                        {
                            throw new InvalidOperationException("Can not convert File to data");
                        }
                    }
                }
            }

            // Validate required fields
            if (!string.IsNullOrWhiteSpace(dto.Name) && !string.IsNullOrWhiteSpace(dto.PhoneNumber))
            {
                resultList.Add(dto);
            }
        }

        if (!resultList.Any())
            return Result.Failure<List<DonatorDto>>(DonatorErrors.Null);
        return Result.Success(resultList);
    }
}
