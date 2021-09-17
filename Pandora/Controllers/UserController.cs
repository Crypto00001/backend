using Microsoft.AspNetCore.Mvc;
using Pandora.Application.Command.Users;
using Pandora.Application.Contract;
using Pandora.Application.Service;
using Pandora.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora.Controllers
{
    [Authorize]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private IJwtUtils _jwtUtils;
        public UserController(IUserService userService, IJwtUtils jwtUtils)
        {
            _userService = userService;
            _jwtUtils = jwtUtils;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(LoginCommand command)
        {
            var user = _userService.Authenticate(command);
            AuthenticateResponse response = new AuthenticateResponse()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = _jwtUtils.GenerateToken(user)
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(CreateUserCommand command)
        {
            _userService.Add(command);
            return Ok(new { message = "Registration successful" });
        }

    }
}
