using System;
namespace GeoService.models
{
	public class UserImage
	{
        public long id { get; set; }
        public int user_id { get; set; }
        public string? user_fullname { get; set; }
        public string? image { get; set; }
        public int site_id { get; set; }
        public string? site_name { get; set; }
        public int zone_id { get; set; }
        public string? zone_name { get; set; }
        public string? created_at { set; get; }
    }
}

