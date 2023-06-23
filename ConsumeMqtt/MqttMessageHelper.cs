
using MQTTnet;
using MQTTnet.Client;
using System;
using ReadFromMqttClient.Model;
using ReadFromMqttClient.Api;

public static class MqttMessageHelper
{
    private const string READFROMMQTT_API_HOST_PORT = "READFROMMQTT_API_HOST_PORT";
    private const string READFROMMQTT_API_PROTOCOL = "READFROMMQTT_API_PROTOCOL";

    //private readonly IReadFromMqttApi _readFromMqttApi;

    public static async Task HandleMessageAsync(MqttApplicationMessageReceivedEventArgs m)
    {
        // What is executed on each message received
        await Console.Out.WriteAsync($"Received message on topic: '{m.ApplicationMessage.Topic}' with content: '{m.ApplicationMessage.ConvertPayloadToString()}'\n\n");
    }
}