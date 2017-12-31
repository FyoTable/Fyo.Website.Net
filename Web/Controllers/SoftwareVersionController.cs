using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Fyo.Enums;
using Fyo.Interfaces;
using Fyo.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using System.Net.Http;
using System.Net;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace Fyo.Controllers
{
    [Route("api/[controller]")]
    public class SoftwareVersionController : Controller
    {
        private ISoftwareVersionService _softwareVersionService;
        private IConfiguration _configuration;
        
        public SoftwareVersionController(ISoftwareVersionService softwareVersionService, IConfiguration configuration ) {
            _softwareVersionService = softwareVersionService;
            _configuration = configuration;
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(long id){
            var softwareVersion = _softwareVersionService.Get(id);
            
            return new OkObjectResult(softwareVersion);
        }
        

        [Authorize]
        [HttpGet]
        public IActionResult GetAll(){
            var softwareVersions = _softwareVersionService.GetAll();
            
            return new OkObjectResult(softwareVersions);
        }
        

        [Authorize]
        [HttpGet]
        [Route("BySoftware/{id}")]
        public IActionResult GetAllBySoftware(long id){
            var softwareVersions = _softwareVersionService.GetAll().Where(sv => sv.ID == id);
            
            return new OkObjectResult(softwareVersions);
        }


        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] SoftwareVersion softwareVersion){
            var newSoftwareVersion = _softwareVersionService.Create(softwareVersion);

            return new OkObjectResult(newSoftwareVersion);
        }

        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(long id, [FromBody] SoftwareVersion softwareVersion){
            var existingSoftwareVersion = _softwareVersionService.Get(id);

            Console.WriteLine("APK?" + softwareVersion.Apk);

            if(existingSoftwareVersion == null){
                return new NotFoundObjectResult(string.Format("No software version found for id: {0}", id));
            }

            existingSoftwareVersion.Version = softwareVersion.Version;
            existingSoftwareVersion.Apk = softwareVersion.Apk;
            existingSoftwareVersion.IsDeleted = softwareVersion.IsDeleted;

            var updatedSoftwareVersion = _softwareVersionService.Update(existingSoftwareVersion);

            return new OkObjectResult(updatedSoftwareVersion);
        }
        
        [HttpGet]
        [Route("{id}/APK")]
        public async Task<IActionResult> APKGet(long id) {
            var existingSoftwareVersion = _softwareVersionService.Get(id);

            var azureConfiguration = _configuration.GetSection("Azure");
            var storageCredentials = new StorageCredentials("fyo", azureConfiguration["StorageKey"]);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var container = cloudBlobClient.GetContainerReference("apks");

            var newBlob = container.GetBlockBlobReference(existingSoftwareVersion.Apk);


            var memStream = new MemoryStream();
            await newBlob.DownloadToStreamAsync(memStream);
            memStream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(memStream, new MediaTypeHeaderValue(newBlob.Properties.ContentType))
            {
                FileDownloadName = existingSoftwareVersion.Apk
            };            
        }

        [Authorize]
        [HttpPost]
        [Route("{id}/APK")]
        public async Task<IActionResult> APK(long id) {
            var existingSoftwareVersion = _softwareVersionService.Get(id);

            var request = HttpContext.Request;

            if(request.Form.Files.Count > 1) {
                return new OkObjectResult(false);
            }
            
            Console.WriteLine("Total Files:" + request.Form.Files.Count);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();
            Console.WriteLine(filePath);

            foreach (var formFile in request.Form.Files)
            {
                Console.WriteLine(formFile.FileName);
                if (formFile.Length > 0)
                {
                    // using (var stream = new FileStream(filePath, FileMode.Create))
                    // {
                    //     await formFile.CopyToAsync(stream);

                        // upload to azure blob storage

                        var azureConfiguration = _configuration.GetSection("Azure");

                        var storageCredentials = new StorageCredentials("fyo", azureConfiguration["StorageKey"]);
                        var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
                        var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

                        var container = cloudBlobClient.GetContainerReference("apks");
                        await container.CreateIfNotExistsAsync();


                        var newBlob = container.GetBlockBlobReference(existingSoftwareVersion.Apk);
                        await newBlob.UploadFromStreamAsync(formFile.OpenReadStream());
                        //await newBlob.UploadFromFileAsync(@"path\myfile.png");
                        return new OkObjectResult(newBlob.Uri.AbsolutePath);
                        
                    //}
                }
            }

            return new OkObjectResult(true);
        }
    }
}
