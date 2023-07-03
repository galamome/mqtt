using MQTTnet.Client;
using MQTTnet;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ConsumeMqtt.Service;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

// Setup the Dependency Injection
var serviceProvider = new ServiceCollection()
    // Configure console logging
    .AddLogging(c => c.AddConsole(opt => opt.LogToStandardErrorThreshold = LogLevel.Debug))
    .AddSingleton<IPersistService, FileWriterService>()
    .BuildServiceProvider();


var logger = serviceProvider.GetService<ILoggerFactory>()
    .CreateLogger<Program>();
logger.LogDebug("Starting application");

var _apiService = serviceProvider.GetService<IPersistService>();

Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services
            .AddSingleton<IPersistService, FileWriterService>()
            // Hosted service, that runs until the application is explicitly stopped
            .AddHostedService<MqttSubscribeService>();
    })
    .Build()
    .Run();

logger.LogDebug("All done!");
