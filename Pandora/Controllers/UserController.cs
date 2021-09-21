﻿using Microsoft.AspNetCore.Mvc;
using Pandora.Application.Command.Users;
using Pandora.Application.Contract;
using Pandora.Application.Service;
using Pandora.Application.ViewModel;
using Pandora.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
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
        public ResultResponse Login(LoginCommand command)
        {
            try
            {
                var user = _userService.Authenticate(command);
                AuthenticateResponse response = new AuthenticateResponse()
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = _jwtUtils.GenerateToken(user)
                };
                return new ResultResponse
                {
                    HasError = false,
                    Data = response
                };
            }
            catch (Exception e)
            {
                return new ResultResponse
                {
                    HasError = true,
                    ErrorMessage = e.Message
                };
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(CreateUserCommand command)
        {
            _userService.Add(command);
            return Ok(new { message = "Registration successful" });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpPut]
        public ResultResponse Update(UpdateUserCommand command)
        {
            try
            {
                var user = _userService.Update(command, UserSession.UserId);
                return new ResultResponse
                {
                    HasError = false,
                    Data = user
                };
            }
            catch (Exception e)
            {
                return new ResultResponse
                {
                    HasError = true,
                    ErrorMessage = e.Message
                };
            }
        }

        [HttpPut]
        [Route("Password")]
        public ResultResponse UpdatePassword(UpdatePasswordUserCommand command)
        {
            try
            {
                _userService.UpdatePassword(command, UserSession.UserId);
                return new ResultResponse
                {
                    HasError = false
                };
            }
            catch (Exception e)
            {
                return new ResultResponse
                {
                    HasError = true,
                    ErrorMessage = e.Message
                };
            }
        }
    }
}
