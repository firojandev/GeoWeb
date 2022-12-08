using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GEOAttendance.Controllers
{
    [Route("api/[controller]")]
    public class LoginApiController : Controller
    {
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            return Json("Okay");
           
        }

    }
}

