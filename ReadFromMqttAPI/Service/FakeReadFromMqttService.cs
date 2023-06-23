using ReadFromMqttAPI.Model;

namespace ReadFromMqttAPI.Service;

public sealed class FakeReadFromMqttService : IReadFromMqttService
{
    private HashSet<IRecord> _records = new HashSet<IRecord>();
    private readonly ILogger<FakeReadFromMqttService> _logger;

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<IRecord>> GetAllAsync()
    {
        return _records;
    }

    /// <inheritdoc/>
    public async Task<IRecord> CreateRecordAsync(IRecord record)
    {
        if (_records.Add(record))
        {
            return record;
        }

        return null;
    }


}
