using System;
using System.Configuration;
using GeoService.models;
using Microsoft.Extensions.Configuration;

namespace GeoService.services
{
    public class UserService : IUserService
    {
        private DbHelper _dbHelper;

        private IConfiguration configuration;
        
        public UserService(IConfiguration _configuration) {
            configuration = _configuration;
            _dbHelper = new DbHelper(configuration);
        }

        public async Task<List<UserModel>> GetAll()
        {
            return await _dbHelper.GetUsersList();
        }

        public async Task<UserModel> GetUser(string device_id)
        {
            return await _dbHelper.GetUser(device_id);
        }

        public async Task<bool> SaveUser(UserModel userModel)
        {
            return await _dbHelper.SaveUser(userModel);
        }

        public async Task<Boolean> UpdateUser(int id, string expiredatetime)
        {
            return await _dbHelper.UpdateUser(id, expiredatetime);
        }
    }
}

