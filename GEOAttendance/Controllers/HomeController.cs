using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GEOAttendance.Models;
using GeoService.services;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Components.Forms;
using QRCoder;
using static QRCoder.PayloadGenerator;

namespace GEOAttendance.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private IUserService _iUserService;

    private readonly IWebHostEnvironment _environment;

    //https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/how-to?pivots=dotnet-6-0

    public HomeController(ILogger<HomeController> logger, IUserService iUserService, IWebHostEnvironment environment)
    {
        _logger = logger;
        _iUserService = iUserService;
        _environment = environment;
    }

    public IActionResult Index(int Id)
    {
        QRCodeModel qRCodeModel = new QRCodeModel();
        qRCodeModel.UserId = Id;

        return View(qRCodeModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateQRCode(QRCodeModel qrCodeModel) {

        var userId = qrCodeModel.UserId;
        var expireDateTime = qrCodeModel.ExpireDateTIme.ToString("yyyy-MM-dd hh:mm:ss");

        var isSuccess = _iUserService.UpdateUser(Convert.ToInt32(userId), expireDateTime);


        var qURL = "https://geoattendance.azurewebsites.net/api/LoginApi/" + userId;

        using (MemoryStream memoryStream = new MemoryStream())
        {
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();

            Url WebUri = new Url(qURL);
            string UriPayload = WebUri.ToString();

            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(UriPayload, QRCodeGenerator.ECCLevel.Q);
            QRCoder.QRCode qrCodes = new QRCoder.QRCode(qRCodeData);
            using (Bitmap bitmap = qrCodes.GetGraphic(20))
            {
                bitmap.Save(memoryStream, ImageFormat.Png);
                qrCodeModel.QrCodeUri = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
            }
        }
        return View(qrCodeModel);
    }


    public async Task<IActionResult> Users()
    {
        var ulist = await _iUserService.GetAll();

     return View(ulist);
    }

    [HttpGet]
    public async Task<IActionResult> CreateUser()
    {
        GeoService.models.UserModel userModel = new GeoService.models.UserModel();
        return View(userModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(GeoService.models.UserModel userModel)
    {
        await _iUserService.SaveUser(userModel);

        return Redirect("Users");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

  
}

