using Microsoft.AspNetCore.Mvc;
using Pandora.Application.Command.Payments;
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
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
       
        [AllowAnonymous]
        [HttpPost]
        public async Task<Result> CreateAsync(CreatePaymentCommand command)
        {
            try
            {
                await _paymentService.CreateAsync(command,UserSession.UserId);
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
