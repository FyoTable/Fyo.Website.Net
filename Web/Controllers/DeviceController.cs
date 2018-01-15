using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Fyo.Enums;
using Fyo.Interfaces;
using Fyo.Models;
using Microsoft.EntityFrameworkCore;

namespace Fyo.Controllers
{
    public struct VersionViewModel {
        public long id;
        public string version;
    }

    public class DeviceSoftwareVersionViewModel {
        public long id;
        public long softwareId;
        public string softwareName;
        public string version;
        public VersionViewModel[] allVersions;
    }

    [Route("api/[controller]")]
    public class DeviceController : Controller
    {
        private IDeviceService _deviceService;
        private ISoftwareService _softwareService;
        private ISoftwareVersionService _softwareVersionService;
        private IDeviceSoftwareVersionService _deviceSoftwareVersionService;
        
        public DeviceController(IDeviceService deviceService, ISoftwareVersionService softwareVersionService, ISoftwareService softwareService, IDeviceSoftwareVersionService deviceSoftwareVersionService) {
            _deviceService = deviceService;
            _softwareVersionService = softwareVersionService;
            _softwareService = softwareService;
            _deviceSoftwareVersionService = deviceSoftwareVersionService;
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(long id){
            var device = _deviceService.Get(id);
            
            return new OkObjectResult(device);
        }

        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(long id){
            var device = _deviceService.Get(id);

            if(device != null) {
                _deviceService.Delete(device);
            }
            
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


        [Authorize]
        [HttpGet]
        [Route("softwareVersions/{id}")]
        public IActionResult SoftwareVersions(long id) {
            var versions = _deviceService.SoftwareVersions(id).OrderByDescending(x => x.ID);

            var result = new List<DeviceSoftwareVersionViewModel>();

            foreach(var version in versions) {
                Console.WriteLine("Version: " + version.ID + " : " + version.SoftwareId);

                var software = _softwareService.Get(version.SoftwareId);


                var r = new DeviceSoftwareVersionViewModel();
                r.id = version.ID;
                r.version = version.Version;
                r.softwareId = software.ID;
                r.softwareName = software.Name;
                r.allVersions = _softwareVersionService.GetAll().Where(sv => sv.Software.ID == r.softwareId).Select(sv => new VersionViewModel() {
                    id = sv.ID,
                    version = sv.Version
                }).OrderByDescending(x => x.id).ToArray();
                result.Add(r);
            }

            return new OkObjectResult(result.ToArray());
        }


        [Authorize]
        [HttpPost]
        [Route("{id}/addSoftware/{softwareId}")]
        public IActionResult SoftwareVersions(long id, long softwareId) {
            var device = _deviceService.Get(id);
            var software = _softwareService.Get(softwareId);

            var versions = _softwareVersionService.GetAll().Where(s => s.SoftwareId == softwareId);
            var version = versions.OrderByDescending(x => x.ID).FirstOrDefault();
            if(version == null) {
                return new OkObjectResult(false);
            }

            var devSoftVer = new DeviceSoftwareVersion() {
                Device = device,
                DeviceId = id,
                SoftwareVersionId = version.ID,
                SoftwareVersion = version
            };

            _deviceSoftwareVersionService.Create(devSoftVer);
                        
            return new OkObjectResult(true);
        }


        [HttpGet]
        [Route("config/{id}")]
        public IActionResult Config(string id){
            var device = _deviceService.Get(id);

            var devSoftVer = _deviceSoftwareVersionService.GetAll().Where(d => d.DeviceId == device.ID).Include(x => x.SoftwareVersion.Software);
            
            var software = new List<object>();
            if(devSoftVer != null) {
                foreach(var softVer in devSoftVer) {
                    software.Add(new {
                        id = softVer.SoftwareVersion.Software.Name,
                        version = softVer.SoftwareVersion.Version,
                        apk = softVer.SoftwareVersion.Apk,
                        package = softVer.SoftwareVersion.Software.Package
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
