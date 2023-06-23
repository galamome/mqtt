using ReadFromMqttAPI.Model;

namespace ReadFromMqttAPI.Service;

public interface IReadFromMqttService
{
    /// <summary>
    /// Get all records
    /// </summary>
    /// <returns></returns>
    public Task<IReadOnlyCollection<IRecord>> GetAllAsync();

    /// <summary>
    /// Create a record
    /// </summary>
    /// <param name="record"></param>
    /// <returns></returns>
    public Task<IRecord> CreateRecordAsync(IRecord record);
}