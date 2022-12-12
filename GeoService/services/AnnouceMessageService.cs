using System;
using GeoService.models;
using Microsoft.Extensions.Configuration;

namespace GeoService.services
{
	public class AnnouceMessageService: IAnnouceMessageService
    {
        private DbHelper _dbHelper;

        private IConfiguration configuration;

        public AnnouceMessageService(IConfiguration _configuration)
        {
            configuration = _configuration;
            _dbHelper = new DbHelper(configuration);
        }

        public async Task<List<AnnouceMessageModel>> GetAll()
        {
            return await _dbHelper.GetMessagesList();
        }

        public async Task<AnnouceMessageModel> SaveMessage(AnnouceMessageModel annouceMessageModel)
        {

            AnnouceMessageModel model = await _dbHelper.SaveMessage(annouceMessageModel);

            return model;

        }
    }


}

