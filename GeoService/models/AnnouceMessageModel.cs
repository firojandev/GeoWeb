using System;
namespace GeoService.models
{
	public class AnnouceMessageModel
	{
        public long id { set; get; }
        public long user_id { set; get; }
        public string user_fullname { set; get; }
        public string message { set; get; }
        public string created_at { set; get; }
    }
}

