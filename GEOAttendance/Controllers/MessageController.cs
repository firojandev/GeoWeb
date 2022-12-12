using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEOAttendance.Models;
using GEOAttendance.Services;
using GeoService.models;
using GeoService.services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GEOAttendance.Controllers
{
    public class MessageController : Controller
    {
        private IGeoPublisher _iGeoPublisher;
        private IAnnouceMessageService _iAnnouceMessageService;

        public MessageController(ILogger<HomeController> logger, IGeoPublisher iGeoPublisher, IAnnouceMessageService iAnnouceMessageService)
        {
            _iGeoPublisher = iGeoPublisher;
            _iAnnouceMessageService = iAnnouceMessageService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var list = await _iAnnouceMessageService.GetAll();

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Create(long Id, string fullname, string deviceId)
        {
            AnnouceMessage messageModel = new AnnouceMessage();
            messageModel.user_id = Id;
            messageModel.device_id = deviceId;
            messageModel.user_fullname = fullname;

            return View(messageModel);
        }

        [HttpPost]
        public async Task<IActionResult> sendMessage(AnnouceMessage msg)
        {

            AnnouceMessageModel AnnouceMessageModel = new AnnouceMessageModel();
            AnnouceMessageModel.user_id = msg.user_id;
            AnnouceMessageModel.user_fullname = msg.user_fullname;
            AnnouceMessageModel.message = msg.message;

            AnnouceMessageModel model = await _iAnnouceMessageService.SaveMessage(AnnouceMessageModel);

            msg.id = model.id;
            msg.created_at = model.created_at;

            await _iGeoPublisher.sendMessage(msg);

            return Redirect("Index");

        }
    }
}

