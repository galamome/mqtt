using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;

namespace ConsumeMqtt.Service;

internal sealed class MqttSubscribeService : IHostedService
{
    private readonly ILogger<MqttSubscribeService> _logger;
    private readonly IPersistService _persistService;

    public MqttSubscribeService(ILoggerFactory loggerFactory,
                IPersistService persistService)
    {
        _logger = loggerFactory.CreateLogger<MqttSubscribeService>();
        _persistService = persistService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        const string MQTT_HOSTNAME = "MQTT_HOSTNAME";
        const string MQTT_PORT = "MQTT_PORT";
        const string MQTT_TOPIC_NAME = "MQTT_TOPIC_NAME";

        var hostname = Environment.GetEnvironmentVariable(MQTT_HOSTNAME);
        var portString = Environment.GetEnvironmentVariable(MQTT_PORT);
        var topicName = Environment.GetEnvironmentVariable(MQTT_TOPIC_NAME);

        // Default MQTT port is 1883, unless othe value is specified
        int port = 1883;

        if (int.TryParse(portString, out int portFromEnv))
        {
            port = portFromEnv;
        }
        else
        {
            _logger.LogWarning($"Port '{portString}' cannot be parsed as integer from environment variable {MQTT_PORT}, default port for MQTT will be used {port}");
        }

        /*
        string clientId = "client1-session1";  //client ID can be the session identifier.  A client can have multiple sessions using username and clientId.
        string x509_pem = @" client certificate cer.pem file path\client.cer.pem";  //Provide your client certificate .cer.pem file path
        string x509_key = @"client certificate key.pem file path\client.key.pem";  //Provide your client certificate .key.pem file path

        var certificate = new X509Certificate2(X509Certificate2.CreateFromPemFile(x509_pem, x509_key).Export(X509ContentType.Pkcs12));
        */

        var mqttClient = new MqttFactory().CreateMqttClient();

        var connAck = await mqttClient!.ConnectAsync(new MqttClientOptionsBuilder()
            .WithTcpServer(hostname, port)
            /*
            .WithClientId(clientId)
            .WithCredentials("client1-authnID", "")  //use client authentication name in the username
            .WithTls(new MqttClientOptionsBuilderTlsParameters()
            {
                UseTls = true,
                Certificates = new X509Certificate2Collection(certificate)
            })
            */
            .Build());

        Console.WriteLine($"Client Connected: {mqttClient.IsConnected} with CONNACK: {connAck.ResultCode}");

        mqttClient.ApplicationMessageReceivedAsync += async (m) =>
        {
            // What is executed on each message received
            await _persistService.CreateRecordAsync(m);
        };

        var suback = await mqttClient.SubscribeAsync(topicName);
        suback.Items.ToList().ForEach(s => Console.WriteLine($"subscribed to '{s.TopicFilter.Topic}' with '{s.ResultCode}'"));
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}