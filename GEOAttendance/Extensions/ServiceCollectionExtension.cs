﻿using System;
using GEOAttendance.Services;
using MQTTnet.Client;

namespace GEOAttendance.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddMqttClientHostedService(this IServiceCollection services)
        {
            services.AddMqttClientServiceWithConfig(aspOptionBuilder =>
            {
                aspOptionBuilder
                .WithCredentials("", "")
                .WithClientId("")
                .WithTcpServer("", 0)
                .WithTls();
            });
            return services;
        }

        private static IServiceCollection AddMqttClientServiceWithConfig(this IServiceCollection services, Action<MqttClientOptionsBuilder> configure)
        {
            services.AddSingleton<MqttClientOptions>(serviceProvider =>
            {
                var optionBuilder = new MqttClientOptionsBuilder();
                configure(optionBuilder);
                return optionBuilder.Build();
            });
            services.AddSingleton<MqttClientService>();
            services.AddSingleton<IHostedService>(serviceProvider =>
            {
                return serviceProvider.GetService<MqttClientService>();
            });
            services.AddSingleton<MqttClientServiceProvider>(serviceProvider =>
            {
                var mqttClientService = serviceProvider.GetService<MqttClientService>();
                var mqttClientServiceProvider = new MqttClientServiceProvider(mqttClientService);
                return mqttClientServiceProvider;
            });
            return services;
        }
    }
}

