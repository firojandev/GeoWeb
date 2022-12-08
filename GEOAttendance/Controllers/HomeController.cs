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

namespace GEOAttendance.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private IUserService _iUserService;

    private readonly IWebHostEnvironment _environment;

    public HomeController(ILogger<HomeController> logger, IUserService iUserService, IWebHostEnvironment environment)
    {
        _logger = logger;
        _iUserService = iUserService;
        _environment = environment;
    }

    public IActionResult Index(int userId)
    {
        QRCodeModel qRCodeModel = new QRCodeModel();
        qRCodeModel.UserId = userId;

        return View(qRCodeModel);
    }

    [HttpPost]
    public IActionResult CreateQRCode(QRCodeModel qrCodeModel) {

        //as per requirement modify here
        qrCodeModel.QRCodeText = qrCodeModel.UserId + "/" + qrCodeModel.QRCodeText;

        using (MemoryStream memoryStream = new MemoryStream())
        {
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(qrCodeModel.QRCodeText, QRCodeGenerator.ECCLevel.Q);
            QRCoder.QRCode qrCodes = new QRCoder.QRCode(qRCodeData);
            using (Bitmap bitmap = qrCodes.GetGraphic(20))
            {
                bitmap.Save(memoryStream, ImageFormat.Png);
                qrCodeModel.QrCodeUri = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
                //ViewBag.QRCode = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
            }
        }
        return View(qrCodeModel);
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

