using System;
using GeoService.models;

namespace GeoService.services
{
	public interface IUserLocation
	{
        Task<List<GeoService.models.UserLocation>> GetAll();
        Task<Boolean> SaveLocation(GeoService.models.UserLocation userLocation);
    }
}

