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
    [Route("apk/")]
    public class ApkController : Controller
    {
        private ISoftwareVersionService _softwareVersionService;
        private IConfiguration _configuration;
        
        public ApkController(ISoftwareVersionService softwareVersionService, IConfiguration configuration ) {
            _softwareVersionService = softwareVersionService;
            _configuration = configuration;
        }
        
        [HttpGet]
        [Route("{apk}")]
        public async Task<IActionResult> Get(string apk) {

            var azureConfiguration = _configuration.GetSection("Azure");
            var storageCredentials = new StorageCredentials("fyo", azureConfiguration["StorageKey"]);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var container = cloudBlobClient.GetContainerReference("apks");

            Console.WriteLine("Looking for blob: " + apk);
            var newBlob = container.GetBlockBlobReference(apk);

            var memStream = new MemoryStream();
            await newBlob.DownloadToStreamAsync(memStream);
            memStream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(memStream, new MediaTypeHeaderValue(newBlob.Properties.ContentType))
            {
                FileDownloadName = apk
            };            
        }
    }
}
