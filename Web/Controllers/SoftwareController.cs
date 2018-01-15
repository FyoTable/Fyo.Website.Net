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
    public class SoftwareAllViewModel {
        public Software Software { get; set; }
        public SoftwareVersion[] SoftwareVersions { get; set; }
    }

    [Route("api/[controller]")]
    public class SoftwareController : Controller
    {
        private ISoftwareService _softwareService;
        private ISoftwareVersionService _softwareVersionService;
        
        public SoftwareController(ISoftwareService softwareService, ISoftwareVersionService softwareVersionService) {
            _softwareService = softwareService;
            _softwareVersionService = softwareVersionService;
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(long id){
            var software = _softwareService.Get(id);
            
            return new OkObjectResult(software);
        }
        

        [Authorize]
        [HttpGet]
        public IActionResult GetAll(){
            var software = _softwareService.GetAll();

            var result = new List<SoftwareAllViewModel>();
            foreach(var s in software) {
                result.Add(new SoftwareAllViewModel() {
                    Software = s,
                    SoftwareVersions = _softwareVersionService.GetAll().Where(sv => sv.SoftwareId == s.ID).ToArray()
                });
            }
            
            return new OkObjectResult(result.ToArray());
        }


        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] Software software){
            var newSoftware = _softwareService.Create(software);

            return new OkObjectResult(newSoftware);
        }

        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(long id, [FromBody] Software device){
            var existingSoftware = _softwareService.Get(id);

            if(existingSoftware == null){
                return new NotFoundObjectResult(string.Format("No device found for id: {0}", id));
            }

            existingSoftware.Name = device.Name;
            existingSoftware.IsDeleted = device.IsDeleted;
            existingSoftware.Package = device.Package;

            var updatedSoftware = _softwareService.Update(existingSoftware);

            return new OkObjectResult(updatedSoftware);
        }
    }
}
