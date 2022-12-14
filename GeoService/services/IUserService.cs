using System;
using GeoService.models;

namespace GeoService.services
{
	public interface IUserService
	{
		Task<List<UserModel>> GetAll();
        Task<UserModel> GetUser(int id);
        Task<Boolean> UpdateUser(int id,string expiredatetime);
    }
}

