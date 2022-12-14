using System;
using MQTTnet;
using MQTTnet.Client;
using System.Text;
using GEOAttendance.Models;
using System.Text.Json;
using GeoService.services;
using GeoService.models;

namespace GEOAttendance.Services
{
    public class MqttClientService : IMqttClientService
    {
        private readonly IMqttClient mqttClient;
        private readonly MqttClientOptions options;
        private readonly ILogger<MqttClientService> _logger;

        private IUserLocation _iUserLocation;

        public MqttClientService(MqttClientOptions options, ILogger<MqttClientService> logger, IUserLocation iUserLocation)
        {
            this.options = options;
            mqttClient = new MqttFactory().CreateMqttClient();
            _logger = logger;
            _iUserLocation = iUserLocation;
            ConfigureMqttClient();
        }

        private void ConfigureMqttClient()
        {
            mqttClient.ConnectedAsync += HandleConnectedAsync;
            mqttClient.DisconnectedAsync += HandleDisconnectedAsync;
            mqttClient.ApplicationMessageReceivedAsync += HandleApplicationMessageReceivedAsync;
        }

        public async Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
        {

            MqttApplicationMessage applicationMessage = eventArgs.ApplicationMessage;

            var topic = applicationMessage.Topic;
            var messageBytes = applicationMessage.Payload;
            string message = Encoding.UTF8.GetString(messageBytes);

            Console.WriteLine(topic);
            Console.WriteLine(message);

            switch (topic)
            {
                case "location":

                    GeoService.models.UserLocation uLocation = JsonSerializer.Deserialize<GeoService.models.UserLocation>(message)!;

                    await _iUserLocation.SaveLocation(uLocation);

                    break;

                default:
                    break;

            }

            return;

        }

        public async Task HandleConnectedAsync(MqttClientConnectedEventArgs eventArgs)
        {
            _logger.LogInformation("connected");
            await mqttClient.SubscribeAsync("location");

        }

        public async Task HandleDisconnectedAsync(MqttClientDisconnectedEventArgs eventArgs)
        {

            _logger.LogInformation("HandleDisconnected");
            #region Reconnect_Using_Event :https://github.com/dotnet/MQTTnet/blob/master/Samples/Client/Client_Connection_Samples.cs
            /*
            * This sample shows how to reconnect when the connection was dropped.
            * This approach uses one of the events from the client.
            * This approach has a risk of dead locks! Consider using the timer approach (see sample).
            * The following reconnection code "Reconnect_Using_Timer" is recommended
           */
            //if (eventArgs.ClientWasConnected)
            //{
            //    // Use the current options as the new options.
            //    await mqttClient.ConnectAsync(mqttClient.Options);
            //}
            #endregion
            await Task.CompletedTask;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await mqttClient.ConnectAsync(options);

            #region Reconnect_Using_Timer:https://github.com/dotnet/MQTTnet/blob/master/Samples/Client/Client_Connection_Samples.cs
            /* 
             * This sample shows how to reconnect when the connection was dropped.
             * This approach uses a custom Task/Thread which will monitor the connection status.
            * This is the recommended way but requires more custom code!
           */
            _ = Task.Run(
           async () =>
           {
               // // User proper cancellation and no while(true).
               while (true)
               {
                   try
                   {
                       // This code will also do the very first connect! So no call to _ConnectAsync_ is required in the first place.
                       if (!await mqttClient.TryPingAsync())
                       {
                           await mqttClient.ConnectAsync(mqttClient.Options, CancellationToken.None);

                           // Subscribe to topics when session is clean etc.
                           _logger.LogInformation("The MQTT client is connected.");
                       }
                   }
                   catch (Exception ex)
                   {
                       // Handle the exception properly (logging etc.).
                       _logger.LogError(ex, "The MQTT client  connection failed");
                   }
                   finally
                   {
                       // Check the connection state every 5 seconds and perform a reconnect if required.
                       await Task.Delay(TimeSpan.FromSeconds(5));
                   }
               }
           });
            #endregion

        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                var disconnectOption = new MqttClientDisconnectOptions
                {
                    Reason = MqttClientDisconnectReason.NormalDisconnection,
                    ReasonString = "NormalDiconnection"
                };
                await mqttClient.DisconnectAsync(disconnectOption, cancellationToken);
            }
            await mqttClient.DisconnectAsync();
        }

        public async Task<string> sendMessage(AnnouceMessage annouceMessage)
        {
            string jsonString = JsonSerializer.Serialize<AnnouceMessage>(annouceMessage);

            if (mqttClient.IsConnected)
            {
                await mqttClient.PublishStringAsync(annouceMessage.device_id+"/annouce", jsonString);
            }
            else
            {
                Console.WriteLine("MQTT client is not connected");

            }

            return "NotNeeded";
        }
    }
}

