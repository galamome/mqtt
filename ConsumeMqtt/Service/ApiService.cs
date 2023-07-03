using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System;
using ReadFromMqttClient.Model;
using ReadFromMqttClient.Api;
using Microsoft.Extensions.Logging;

using Microsoft.Extensions.DependencyInjection;
using MQTTnet.Client;
using MQTTnet;

namespace ConsumeMqtt.Service;

public sealed class ApiService : IPersistService
{
    private const string READFROMMQTT_API_HOST_PORT = "READFROMMQTT_API_HOST_PORT";
    private const string READFROMMQTT_API_PROTOCOL = "READFROMMQTT_API_PROTOCOL";

    private readonly IReadFromMqttApi _readFromMqttApi;

    private readonly ILogger<ApiService> _logger;

    public ApiService(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<ApiService>();

        var hostAndPort = Environment.GetEnvironmentVariable(READFROMMQTT_API_HOST_PORT);
        var protocol = Environment.GetEnvironmentVariable(READFROMMQTT_API_PROTOCOL);

        if (!String.IsNullOrEmpty(hostAndPort) && !String.IsNullOrEmpty(protocol))
        {
            var uri = $"{protocol}://{hostAndPort}";
            _logger.LogInformation($"Access to read from MQTT API initialized with URI: {uri}");
            _readFromMqttApi = new ReadFromMqttApi(uri);
        }
        else
        {
            var error = $"Cannot initialize ApiService";
            if (String.IsNullOrEmpty(hostAndPort))
            {
                error += $" because environment variable is not set '{READFROMMQTT_API_HOST_PORT}";
            }
            if (String.IsNullOrEmpty(protocol))
            {
                error += $" because environment variable is not set '{READFROMMQTT_API_PROTOCOL}";
            }
            _logger.LogError(error);
            throw new Exception(error);
        }
    }

    /// <inheritdoc/>
    /*
    public async Task<ReadOnlyCollection<RecordDto>> GetAllAsync()
    {
        var allRecs = await _readFromMqttApi.ReadFromMqttGetAllRecordsAsync();
        return allRecs.AsReadOnly();
    }
    */

    /// <inheritdoc/>
    public async Task CreateRecordAsync(MqttApplicationMessageReceivedEventArgs m)
    {
        // What is executed on each message received
        var record = new RecordDto(DateTime.Now, m.ApplicationMessage.Topic, m.ApplicationMessage.ConvertPayloadToString());
        _logger.LogInformation($"Will post new record {record}");
        await _readFromMqttApi.ReadFromMqttCreateRecordAsync(record);
    }
}