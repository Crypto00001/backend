using Pandora.Domain.Domain;
using Pandora.Domain.Repository;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using Pandora.Application.Contract;
using System;
using System.Linq;
using Pandora.Application.ViewModel;
using System.Threading.Tasks;

namespace Pandora.Application.Service
{
    public class WalletService : IWalletService
    {
        private readonly WalletRepository _walletRepository;
        public WalletService(WalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<List<WalletViewModel>> GetAll(Guid userId)
        {
            return (await _walletRepository.GetAll(userId)).Select(q => new WalletViewModel
            {
                Balance = q.Balance,
                InvestedBalance = q.InvestedBalance,
                WalletAddress = q.WalletAddress,
                WalletType = q.WalletType,
                AvailableBalance = q.AvailableBalance
            }).ToList();
        }
    }
}
