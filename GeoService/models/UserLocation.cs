using System;
namespace GeoService.models
{
	public class UserLocation
	{
        public long id { get; set; }
        public int user_id { get; set; }
        public string user_fullname { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public int site_id { get; set; }
        public string? site_name { get; set; }
        public int zone_id { get; set; }
        public string? zone_name { get; set; }
        public string created_at { set; get; }


    }
}

