using MQTTnet.Client;

public interface IPersistService
{
    /// <summary>
    /// Get the collection of all Records from the API
    /// </summary>
    /// <returns></returns>
    /*
    public Task<ReadOnlyCollection<RecordDto>> GetAllAsync();
    */

    /// <summary>
    /// Persists a record from a MQTT message
    /// </summary>
    /// <param name="m">MQTT message</param>
    /// <returns></returns>
    public Task CreateRecordAsync(MqttApplicationMessageReceivedEventArgs m);

}