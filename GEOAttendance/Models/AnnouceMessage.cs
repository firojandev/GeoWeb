using System;
namespace GEOAttendance.Models
{
	public class AnnouceMessage
	{
		public long id { set; get; }
        public long user_id { set; get; }
        public string user_fullname { set; get; }
        public string device_id { set; get; }
        public string message { set; get; }
        public string created_at { set; get; }
    }
}

