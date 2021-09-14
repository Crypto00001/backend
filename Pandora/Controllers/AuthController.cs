using Microsoft.AspNetCore.Mvc;
using Pandora.Application.Command.Users;
using Pandora.Application.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserApplicaitonService _userApplicaitonService;

        public AuthController(UserApplicaitonService userApplicaitonService)
        {
            _userApplicaitonService = userApplicaitonService;
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(CreateUserCommand command)
        {
            _userApplicaitonService.Add(command);
            return Ok();
        }

        [HttpPost]
        [Route("ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }
    }
}
