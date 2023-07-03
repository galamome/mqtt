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

public sealed class FileWriterService : IPersistService
{
    private const string FILE_TO_WRITE = "FILE_TO_WRITE";

    private readonly ILogger<FileWriterService> _logger;

    private readonly FileInfo _fileToWrite;

    public FileWriterService(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<FileWriterService>();

        var filePath = Environment.GetEnvironmentVariable(FILE_TO_WRITE);

        if (!String.IsNullOrEmpty(filePath))
        {
            _fileToWrite = new FileInfo(filePath);
            _logger.LogInformation($"File to be written is '{filePath}'");
        }
        else
        {
            var error = $"Cannot initialize FileWriterService";
            if (String.IsNullOrEmpty(filePath))
            {
                error += $" because environment variable is not set '{FILE_TO_WRITE}";
            }
            _logger.LogError(error);
            throw new Exception(error);
        }
    }

    /// <inheritdoc/>
    public async Task CreateRecordAsync(MqttApplicationMessageReceivedEventArgs m)
    {
        // What is executed on each message received
        var messageContent = m.ApplicationMessage.ConvertPayloadToString();
        _logger.LogInformation($"Message will be appended '{messageContent}'");

        using (var outputFile = File.AppendText(_fileToWrite.FullName))
        {
            await outputFile.WriteLineAsync(messageContent);
        }
    }
}