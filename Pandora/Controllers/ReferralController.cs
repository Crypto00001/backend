using Microsoft.AspNetCore.Mvc;
using Pandora.Application.Command.Referrals;
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
    public class ReferralController : Controller
    {
        private readonly IReferralService _referralService;
        private IJwtUtils _jwtUtils;
        public ReferralController(IReferralService referralService, IJwtUtils jwtUtils)
        {
            _referralService = referralService;
            _jwtUtils = jwtUtils;
        }
       
        [AllowAnonymous]
        [HttpPost]
        public async Task<Result> CreateAsync(CreateReferralCommand command)
        {
            try
            {
                await _referralService.CreateAsync(command, UserSession.UserId);
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
