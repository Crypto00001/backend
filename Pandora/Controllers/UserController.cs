using Microsoft.AspNetCore.Mvc;
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
        public async Task<Result> Login(LoginCommand command)
        {
            try
            {
                var user = await _userService.Authenticate(command);
                AuthenticateResponse response = new AuthenticateResponse()
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = _jwtUtils.GenerateToken(user)
                };
                return new Result
                {
                    HasError = false,
                    Data = response
                };
            }
            catch (Exception e)
            {
                var result = new Result
                {
                    HasError = true,
                    ErrorMessage = e.Message
                };
                if (e.Data.Contains("Captcha"))
                {
                    result.Captcha = (bool)e.Data["Captcha"];
                }

                return result;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<Result> RegisterAsync(CreateUserCommand command, string referralCode)
        {
            try
            {
                await _userService.CreateAsync(command,referralCode);
                return new Result
                {
                    HasError = false
                };
            }
            catch (Exception e)
            {
                var result = new Result
                {
                    HasError = true,
                    ErrorMessage = e.Message
                };

                return result;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [HttpPut]
        public async Task<Result> UpdateAsync(UpdateUserCommand command)
        {
            try
            {
                var user = await _userService.UpdateAsync(command, UserSession.UserId);
                return new Result
                {
                    HasError = false,
                    Data = user
                };
            }
            catch (Exception e)
            {
                return new Result
                {
                    HasError = true,
                    ErrorMessage = e.Message
                };
            }
        }

        [HttpPut]
        [Route("Password")]
        public async Task<Result> UpdatePasswordAsync(UpdatePasswordUserCommand command)
        {
            try
            {
                await _userService.UpdatePassword(command, UserSession.UserId);
                return new Result
                {
                    HasError = false
                };
            }
            catch (Exception e)
            {
                return new Result
                {
                    HasError = true,
                    ErrorMessage = e.Message
                };
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("ResetPasswordRequest")]
        public async Task<Result> ResetPasswordRequestAsync(ResetPasswordRequestCommand command)
        {
            try
            {
                await _userService.ResetPasswordRequest(command);
                return new Result
                {
                    HasError = false
                };
            }
            catch (Exception e)
            {
                return new Result
                {
                    HasError = true,
                    ErrorMessage = e.Message
                };
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("DoResetPassword")]
        public async Task<Result> DoResetPasswordAsync(DoResetPasswordCommand command)
        {
            try
            {
                await _userService.DoResetPassword(command);
                return new Result
                {
                    HasError = false
                };
            }
            catch (Exception e)
            {
                return new Result
                {
                    HasError = true,
                    ErrorMessage = e.Message
                };
            }
        }
    }
}
