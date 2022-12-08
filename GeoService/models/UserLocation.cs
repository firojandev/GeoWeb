using System;
namespace GeoService.models
{
	public class UserLocation
	{
        public long id { get; set; }
        public int UserId { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public int SiteId { get; set; }
        public string? SiteName { get; set; }
        public int ZoneId { get; set; }
        public string? ZoneName { get; set; }
    }
}

