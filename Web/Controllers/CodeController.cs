using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Fyo.Enums;
using Fyo.Interfaces;
using Fyo.Models;

namespace Fyo.Controllers
{
    [Route("api/[controller]")]
    public class CodeController : Controller
    {
        private IDeviceService _deviceService;
        private ICodeService _codeService;
        
        public CodeController(IDeviceService deviceService, ICodeService codeService) {
            _deviceService = deviceService;
            _codeService = codeService;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(string id){
            var code = _codeService.ByCode(id);

            if(code == null) {
                Console.Write("Code not found");
                return new NotFoundObjectResult(false);
            }

            var timespan = DateTime.Now - code.CreatedDate;
            if(timespan.Minutes > 5) {

                Console.Write("Code expired, diff: " + timespan.Minutes);
                return new NotFoundObjectResult(false);
            }

            var device = _deviceService.Get(code.DeviceId);
            
            return new OkObjectResult(new {
                id = device.UniqueIdentifier,
                player = code.Player
            });
        }

        [HttpPost]
        [Route("create/{id}/{player}")]
        public IActionResult Create(string id, int player){

            var device = _deviceService.Get(id);

            if(device == null) {
                return new NotFoundObjectResult(false);
            }

            var digits = "";
            var r = new Random();
            for(var i = 0; i < 7; i++) {
                digits += r.Next(10);
            }
            var code = _codeService.Create(new Code() {
                Device = device,
                Digits = digits,
                Player = player,
                CreatedDate = DateTime.Now
            });

            return new OkObjectResult(digits);
        }

    }
}
