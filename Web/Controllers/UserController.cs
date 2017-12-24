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
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService) {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAll(){
            var users = _userService.GetAll();
            return new OkObjectResult(users);
        }

        [HttpPost]
        public IActionResult Create([FromBody] User user){
            return new JsonResult(user);
        }
    }
}
