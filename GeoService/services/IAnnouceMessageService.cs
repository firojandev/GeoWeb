using System;
using GeoService.models;

namespace GeoService.services
{
	public interface IAnnouceMessageService
    {
        Task<List<GeoService.models.AnnouceMessageModel>> GetAll();
        Task<AnnouceMessageModel> SaveMessage(AnnouceMessageModel annouceMessageModel);
    }
}

