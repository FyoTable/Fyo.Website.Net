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
    public class SampleDataController : Controller
    {
        private IEmailService _emailService;

        public SampleDataController(IEmailService emailService) {
            _emailService = emailService;
        }

        [Authorize]
        [HttpGet]
        [Route("ping/secure")]
        public IActionResult PingSecured()
        {
            return new OkObjectResult("All good. You only get this message if you are authenticated.");
        }

        [HttpGet]
        [Route("ping/insecure")]
        public IActionResult PingInsecured()
        {
            return new OkObjectResult("All good. You should always be able to get this message");
        }

        [HttpGet]
        [Route("questions")]
        public IActionResult GetQuestions(){

            // var options = new List<QuestionOption>();
            // // options.Add(new QuestionOption() { Text = "Agree" });
            // // options.Add(new QuestionOption() { Text = "Somewhat Agree" });
            // // options.Add(new QuestionOption() { Text = "Neutral" });
            // // options.Add(new QuestionOption() { Text = "Somewhat Disagree" });
            // // options.Add(new QuestionOption() { Text = "Disagree" });

            // return new OkObjectResult(new List<Question>(){
            //     new Question(){
            //         ID = 1,
            //         QuestionType = QuestionType.Binary
            //     },
            //     new Question(){
            //         ID = 2,
            //         QuestionType = QuestionType.Scale
            //     }
            // });
            return new OkObjectResult(true);
            
        }


        [HttpGet]
        [Route("survey")]
        public IActionResult GetSurvey(string id){

            return new OkObjectResult(id);
            
        }

        [HttpGet]
        [Route("superadmin")]
        [Authorize(Roles="SuperAdmin")] 
        public IActionResult GetSuperAdmin(){
            return new OkObjectResult("super admins only");
        }

        [HttpGet]
        [Route("admin")]
        [Authorize(Roles="SuperAdmin,Administrator")] 
        public IActionResult GetAdmin(){
            return new OkObjectResult("super admins and admins only");
        }

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles="SuperAdmin,Administrator,Manager")] 
        public IActionResult GetManager(){
            return new OkObjectResult("super admins, admins, and managers only");
        }

        [HttpGet]
        [Route("user")]
        [Authorize(Roles="SuperAdmin,Administrator,Manager,User")] 
        public IActionResult GetUser(){
            return new OkObjectResult("super admins, admins, managers, and users only");
        }

        [HttpGet]
        [Route("email")]
        public IActionResult Email(string id){
            //_emailService.Send(new string[] { "gambitsunob@gmail.com" }, "me@ghoofman.com", "body");

            // _emailService.SendSurvey(new SurveyInstance {
            //     User = new User {
            //         FirstName = "Garrett",
            //         LastName = "Hoofman",
            //         Email = "gambitsunob@gmail.com"
            //     }
            // });
            return new OkObjectResult(id);
        }
    }
}
