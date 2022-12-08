using System;
using GeoService.models;

namespace GeoService.services
{
	public interface IUserService
	{
		Task<List<UserModel>> GetAll();
	}
}

