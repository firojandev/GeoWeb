using System;
using GEOAttendance.Models;

namespace GEOAttendance.Services
{
    public class GeoPublisher : IGeoPublisher
    {
        private readonly IMqttClientService mqttClientService;

        public GeoPublisher(MqttClientServiceProvider provider)
        {
            mqttClientService = provider.MqttClientService;
        }

        public async Task<string> sendMessage(AnnouceMessage annouceMessage)
        {
            await mqttClientService.sendMessage(annouceMessage);

            return null;
        }
    }
}

