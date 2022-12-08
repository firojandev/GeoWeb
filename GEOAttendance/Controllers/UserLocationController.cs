using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoService.services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GEOAttendance.Controllers
{
    public class UserLocationController : Controller
    {
        private IUserLocation _iUserLocation;

        public UserLocationController(IUserLocation iUserLocation)
        {
            _iUserLocation = iUserLocation;
        }


        public async Task<IActionResult> Index()
        {
            var list = await _iUserLocation.GetAll();

            return View(list);
        }
    }
}

