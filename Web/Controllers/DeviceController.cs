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
    public class DeviceController : Controller
    {
        private IDeviceService _deviceService;
        
        public DeviceController(IDeviceService deviceService) {
            _deviceService = deviceService;
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(long id){
            var device = _deviceService.Get(id);
            
            return new OkObjectResult(device);
        }
        

        [Authorize]
        [HttpGet]
        public IActionResult GetAll(){
            var devices = _deviceService.GetAll();
            
            return new OkObjectResult(devices);
        }


        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] Device device){
            device.UniqueIdentifier = Guid.NewGuid();
            var newDevice = _deviceService.Create(device);

            return new OkObjectResult(newDevice);
        }

        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(long id, [FromBody] Device device){
            var existingDevice = _deviceService.Get(id);

            if(existingDevice == null){
                return new NotFoundObjectResult(string.Format("No device found for id: {0}", id));
            }

            existingDevice.Name = device.Name;
            existingDevice.IsDeleted = device.IsDeleted;

            var updatedDevice = _deviceService.Update(existingDevice);

            return new OkObjectResult(updatedDevice);
        }


        [HttpGet]
        [Route("config/{id}")]
        public IActionResult Config(string id){
            var device = _deviceService.Get(id);
            
            var software = new List<object>();
            if(device.DeviceSoftwareVersions != null) {
                foreach(var softVer in device.DeviceSoftwareVersions) {
                    software.Add(new {
                        id = softVer.SoftwareVersion.Software.Name,
                        version = softVer.SoftwareVersion.Version,
                        apk = softVer.SoftwareVersion.APK,
                        url = softVer.SoftwareVersion.URL
                    });
                }
            }
            

            return new OkObjectResult(new {
                name = device.Name,
                config = new {
                    wireless = new {
                        ap = device.WirelessAccessPoint,
                        apIP = device.WirelessAccessPointIP,
                        deviceIP = device.IPAddress
                    },
                    software = software.ToArray()
                }
            });
        }

    }
}
