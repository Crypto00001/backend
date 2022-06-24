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
using Pandora.Application.Scheduler;

namespace Pandora.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly CheckPaymentConfirmationScheduler _checkPaymentConfirmationScheduler;

        public PaymentController(IPaymentService paymentService, CheckPaymentConfirmationScheduler checkPaymentConfirmationScheduler)
        {
            _paymentService = paymentService;
            _checkPaymentConfirmationScheduler = checkPaymentConfirmationScheduler;
        }
       
        [AllowAnonymous]
        [HttpPost]
        public async Task<Result> CreateAsync(CreatePaymentCommand command)
        {
            try
            {
                var data = await _paymentService.CreateAsync(command,UserSession.UserId, _checkPaymentConfirmationScheduler);
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
