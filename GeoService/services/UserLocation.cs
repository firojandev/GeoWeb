using System;
using GeoService.models;

using Microsoft.Extensions.Configuration;

namespace GeoService.services
{
	public class UserLocation: IUserLocation
	{
        private DbHelper _dbHelper;

        private IConfiguration configuration;

        public UserLocation(IConfiguration _configuration)
        {
            configuration = _configuration;
            _dbHelper = new DbHelper(configuration);
        }


        public async Task<List<GeoService.models.UserLocation>> GetAll()
        {
            return await _dbHelper.GetUserLocationsList();
        }
    }
}

