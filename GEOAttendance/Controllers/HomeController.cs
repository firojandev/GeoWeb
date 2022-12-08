using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GEOAttendance.Models;
using GeoService.services;

namespace GEOAttendance.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private IUserService _iUserService;

    public HomeController(ILogger<HomeController> logger, IUserService iUserService)
    {
        _logger = logger;
        _iUserService = iUserService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Users()
    {
        var ulist = await _iUserService.GetAll();

     return View(ulist);
    }

  
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

