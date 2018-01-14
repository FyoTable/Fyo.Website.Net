using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Fyo.Interfaces;
using Fyo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fyo.Controllers
{
    public class CodeViewModel {
        public Device Device { get; set; }
        public Code Code { get; set; }
    }

    public class HomeController : Controller
    {
        private ICodeService _codeService;
        private IDeviceService _deviceService;

        public HomeController(ICodeService codeService, IDeviceService deviceService) {
            _codeService = codeService;
            _deviceService = deviceService;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        
        [Route("/portal")]
        [Route("/portal/{*url}")]
        public IActionResult Portal()
        {
            return View();
        }
        
        [Route("/code/{code}")]
        public IActionResult Code(string code)
        {
            if(code == "999") {
                return View(new CodeViewModel() {
                    Device = new Device(),
                    Code = new Code()
                });
            }

            var c = _codeService.ByCode(code);
            if(c == null) {
                return Redirect("/");
            }

            var timespan = DateTime.Now - c.CreatedDate;
            if(timespan.Minutes > 5) {

                Console.Write("Code expired, diff: " + timespan.Minutes);
                return Redirect("/");
            }

            var device = _deviceService.Get(c.DeviceId);

            return View(new CodeViewModel() {
                Device = device,
                Code = c
            });
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
