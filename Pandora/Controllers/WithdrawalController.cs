using Microsoft.AspNetCore.Mvc;
using Pandora.Application.Command.Withdraws;
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
    public class WithdrawalController : Controller
    {
        private readonly IWithdrawalService _withdrawalService;
        public WithdrawalController(IWithdrawalService withdrawalService)
        {
            _withdrawalService = withdrawalService;
        }
       
        [AllowAnonymous]
        [HttpPost]
        public async Task<Result> CreateAsync(CreateWithdrawalCommand command)
        {
            try
            {
                var data = await _withdrawalService.CreateAsync(command,UserSession.UserId);
                return new Result
                {
                    HasError = false,
                    Data = data
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
