using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEOAttendance.Services;
using GeoService.services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GEOAttendance.Controllers
{
    public class ImageViewController : Controller
    {
        public IUserImageService _iUserImageService;


        public ImageViewController(IUserImageService iUserImageService)
        {
            _iUserImageService = iUserImageService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = await _iUserImageService.GetAll();

            return View(list);
        }
    }
}

