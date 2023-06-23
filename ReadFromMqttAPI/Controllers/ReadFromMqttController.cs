using Microsoft.AspNetCore.Mvc;
using ReadFromMqttAPI.Dto;
using ReadFromMqttAPI.Service;

namespace ReadFromMqttAPI.Controllers;


[ApiController]
//[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:scopes")]
[Route("[controller]")]
public class ReadFromMqttController : ControllerBase
{
    private const string MimeType = "application/json";

    private readonly ILogger<ReadFromMqttController> _logger;

    private readonly IReadFromMqttService _readFromMqttService;

    public ReadFromMqttController(ILoggerFactory loggerFactory,
                IReadFromMqttService readFromMqttService)
    {
        _logger = loggerFactory.CreateLogger<ReadFromMqttController>();
        _readFromMqttService = readFromMqttService;
    }

    /// <summary>
    /// Get the collection of all records
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RecordDto>>> GetAllRecordsAsync()
    {
        var allRecords = await _readFromMqttService.GetAllAsync();
        if (allRecords.Any())
        {
            return Ok(allRecords.Select((r) => r.ToDto()));
        }

        return Ok(new List<RecordDto>());
    }


    /// <summary>
    /// Create a record with the given data
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> CreateRecordAsync(RecordDto dto)
    {

        var report = await _readFromMqttService.CreateRecordAsync(dto.ToInterface());
        if (report != null)
        {
            return Ok();
        }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

}
