namespace Donators.Controllers;

[Route("api/[controller]/[Action]")]
[ApiController]
public class DonatorController(IDonatorServices donatorServices) : ControllerBase
{
    private readonly string EEPlus = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    private readonly IDonatorServices _DonatorServices = donatorServices;
    [HttpGet]
    public async Task<IActionResult> GenerateExcelTempelte(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var fileName = ExcelGenerator.GetFileName(nameof(DonatorDto));
        var stream = ExcelGenerator.GenerateExcelFile<DonatorDto>(fileName);
        return File(stream , EEPlus , fileName);
    }
}
