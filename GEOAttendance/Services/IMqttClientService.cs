using System;
using GEOAttendance.Models;

namespace GEOAttendance.Services
{
    public interface IMqttClientService : IHostedService
    {
        Task<string> sendMessage(AnnouceMessage annouceMessage);
    }
}

