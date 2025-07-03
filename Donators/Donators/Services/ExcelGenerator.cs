using OfficeOpenXml;
namespace Donators.Services;

public class ExcelGenerator
{

    #region Methods
    public static MemoryStream GenerateExcelFile<T>(string fileName) where T : class
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        // Create a new Excel package
        using (ExcelPackage package = new ExcelPackage())
        {
            // Create a new worksheet
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(fileName);

            // Get the properties of the class
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            // Write headers to the worksheet
            for (int i = 0; i < properties.Length; i++)
            {
                worksheet.Cells[1, i + 1].Value = properties[i].Name;
            }

            // Save the Excel package to a file
            //File.WriteAllBytes(filePath, package.GetAsByteArray());
            var stream = new MemoryStream(package.GetAsByteArray());

            // Set the position of the MemoryStream back to the beginning
            stream.Position = 0;
            return stream;
        }
    }

    public static string GetFileName(string fileName)
    {
        return $"{fileName}-{DateTime.UtcNow:yyyyMMddhhmmssffff}.xlsx";
    }

    #endregion

}
