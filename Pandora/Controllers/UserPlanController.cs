using Microsoft.AspNetCore.Mvc;
using Pandora.Application.Command.UserPlans;
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
    public class UserPlanController : Controller
    {
        private readonly IUserPlanService _userPlanService;
        public UserPlanController(IUserPlanService userPlanService)
        {
            _userPlanService = userPlanService;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<Result> CreateAsync(CreateUserPlanCommand command)
        {
            try
            {
                await _userPlanService.CreateAsync(command, UserSession.UserId);
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
        [AllowAnonymous]
        [HttpGet]
        public async Task<Result> GetAllAsync()
        {
            try
            {
                var userPlans = await _userPlanService.GetAll(UserSession.UserId);
                return new Result
                {
                    HasError = false,
                    Data= userPlans
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
