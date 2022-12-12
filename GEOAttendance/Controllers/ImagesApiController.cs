using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoService.models;
using GeoService.services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GEOAttendance.Controllers
{
    [Route("api/[controller]")]
    public class ImagesApiController : Controller
    {
        private IUserImageService _iUserImageService;

        public ImagesApiController(IUserImageService iUserImageService)
        {
            _iUserImageService = iUserImageService;
        }

        [Route("[action]")]
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<JsonResult> Create([FromForm]UserImage userImage)
        {
            if (userImage == null)
                return Json(false);

            var results = _iUserImageService.SaveImage(userImage);

            return Json(results);
            
        }

      
    }
}

