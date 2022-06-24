using Pandora.Domain.Domain;
using Pandora.Domain.Repository;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using Pandora.Application.Contract;
using System;
using System.Linq;
using Pandora.Application.ViewModel;
using System.Threading.Tasks;
using Pandora.Application.Enums;
using Pandora.Application.Scraper;

namespace Pandora.Application.Service
{
    public class WalletService : IWalletService
    {
        private readonly WalletRepository _walletRepository;

        public WalletService(WalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<IEnumerable<WalletViewModel>> GetAll(Guid userId)
        {
            return (await _walletRepository.GetAll(userId)).Select(q => new WalletViewModel
            {
                Balance = q.Balance,
                InvestedBalance = q.InvestedBalance,
                WalletType = q.Type,
                AvailableBalance = q.AvailableBalance
            });
        }

        public async Task<IEnumerable<WalletForDepositViewModel>> GetAllForDeposit(Guid userId)
        {
            return (await _walletRepository.GetAll(userId)).Select(q => new WalletForDepositViewModel
            {
                Name = Enum.GetName(typeof(WalletType), q.Type),
                Balance = q.Balance,
                WalletAddress = q.Address,
                InvestedBalance = q.InvestedBalance,
                AvailableBalance = q.AvailableBalance,
                WalletType = q.Type,
            }).OrderBy(o => o.WalletType);
        }

        public async Task<IEnumerable<WalletForWithdrawViewModel>> GetAllForWithdraw(Guid userId)
        {
            return (await _walletRepository.GetAll(userId)).Select(q => new WalletForWithdrawViewModel
            {
                Name = Enum.GetName(typeof(WalletType), q.Type),
                Balance = q.Balance,
                WalletType = q.Type,
                AvailableBalance = q.AvailableBalance
            }).OrderBy(o => o.WalletType);
        }
    }
}