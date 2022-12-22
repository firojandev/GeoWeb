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
    public class LocationApiController : Controller
    {
        private IUserLocation _iUserLocation;

        public LocationApiController(IUserLocation iUserLocation)
        {
            _iUserLocation = iUserLocation;
        }
    
        [Route("[action]")]
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<JsonResult> Create([FromForm] GeoService.models.UserLocation userLocation)
        {
            if (userLocation == null)
                return Json(false);

            var results = _iUserLocation.SaveLocation(userLocation);

            return Json(results);

        }
    }
}

