using System;
using GEOAttendance.Models;

namespace GEOAttendance.Services
{
	public interface IGeoPublisher
	{
        Task<string> sendMessage(AnnouceMessage annouceMessage);
    }
}

