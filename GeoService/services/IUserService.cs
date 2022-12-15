using System;
using GeoService.models;

namespace GeoService.services
{
	public interface IUserService
	{
		Task<List<UserModel>> GetAll();
        Task<UserModel> GetUser(int id);
        Task<Boolean> SaveUser(UserModel userModel);
        Task<Boolean> UpdateUser(int id,string expiredatetime);
    }
}

