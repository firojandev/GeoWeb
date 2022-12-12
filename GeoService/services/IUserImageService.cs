using System;
namespace GeoService.services
{
	public interface IUserImageService
	{
        Task<List<GeoService.models.UserImage>> GetAll();
        Task<Boolean> SaveImage(GeoService.models.UserImage userImage);
    }
}

