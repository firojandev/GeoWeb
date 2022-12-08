﻿using System;
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
            return await _dbHelper.GetUsersList(); ;
        }
    }
}
