using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GEOAttendance.Models
{
	public class QRCodeModel
	{
        public long UserId { get; set; }

        [Display(Name = "Expired Date & Time")]
        public DateTime ExpireDateTIme { get; set; } = DateTime.Now;

        public string QrCodeUri { get; set; }
    }
}

