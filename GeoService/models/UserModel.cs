using System;
namespace GeoService.models
{
	public class UserModel
	{
        public int id { get; set; }
        public string? fullname { get; set; }
        public string? device_id { get; set; }
        public string? email { get; set; }
        public string created_at { set; get; }
    }
}

