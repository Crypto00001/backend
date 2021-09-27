using Microsoft.AspNetCore.Mvc;
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
    public class WalletController : Controller
    {
        private readonly IWalletService _walletService;
        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }
       
        [AllowAnonymous]
        [HttpGet]
        public async Task<Result> GetAllAsync()
        {
            try
            {
                var wallets = await _walletService.GetAll(UserSession.UserId);
                return new Result
                {
                    HasError = false,
                    Data= wallets
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
        [HttpGet("Deposit")]
        public async Task<Result> GetAllForDepositAsync()
        {
            try
            {
                var wallets = await _walletService.GetAllForDeposit(UserSession.UserId);
                return new Result
                {
                    HasError = false,
                    Data = wallets
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
        [HttpGet("Withdraw")]
        public async Task<Result> GetAllForWithdrawAsync()
        {
            try
            {
                var wallets = await _walletService.GetAllForWithdraw(UserSession.UserId);
                return new Result
                {
                    HasError = false,
                    Data = wallets
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
