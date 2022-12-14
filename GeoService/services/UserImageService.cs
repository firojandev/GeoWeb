using System;
using GeoService.models;
using Microsoft.Extensions.Configuration;

namespace GeoService.services
{
    public class UserImageService : IUserImageService
    {

        private DbHelper _dbHelper;

        private IConfiguration configuration;

        public UserImageService(IConfiguration _configuration)
        {
            configuration = _configuration;
            _dbHelper = new DbHelper(configuration);
        }


        public async Task<List<GeoService.models.UserImage>> GetAll()
        {
            return await _dbHelper.GetUserImagesList();
        }

        public async Task<bool> SaveImage(GeoService.models.UserImage userImage)
        {

            var res = await _dbHelper.SaveImage(userImage);

            return res;

        }
    }
}

