using Pandora.Domain.Domain;
using Pandora.Domain.Repository;
using BCryptNet = BCrypt.Net.BCrypt;
using Pandora.Application.Contract;
using System;
using Pandora.Application.ViewModel;
using System.Threading.Tasks;
using Pandora.Application.Command.Withdraws;

namespace Pandora.Application.Service
{
    public class WithdrawalService : IWithdrawalService
    {
        private readonly WithdrawalRepository _withdrawRepository;
        private readonly WalletRepository _walletRepository;

        public WithdrawalService(WithdrawalRepository withdrawRepository, WalletRepository walletRepository)
        {
            _withdrawRepository = withdrawRepository;
            _walletRepository = walletRepository;
        }

        public async Task<WithdrawalViewModel> CreateAsync(CreateWithdrawalCommand command, Guid userId)
        {

            Withdrawal withdrawal = new Withdrawal
            {
                Amount = command.Amount,
                WithdrawalNumber = await GetWithdrawalNumberAsync(),
                WalletAddress = command.WalletAddress,
                UserId = userId,
                WalletType = command.WalletType
            };
            var wallet = await _walletRepository.GetUserWalletByType(userId, command.WalletType);

            if (wallet.AvailableBalance <= 0)
                throw new AppException("You have no balance to withdraw");

            if (command.Amount > wallet.AvailableBalance)
                throw new AppException("You could not withdraw more than {0}",wallet.AvailableBalance.ToString("G29"));
            
            await _withdrawRepository.Add(withdrawal);
            wallet.AvailableBalance+=command.Amount;
            wallet.Balance+=command.Amount;
            await _walletRepository.Update(wallet);
            
            return new WithdrawalViewModel(){
                WithdrawalNumber = withdrawal.WithdrawalNumber
            };
        }
        private async Task<string> GetWithdrawalNumberAsync()
        {
            Random generator = new Random();
            string result;
            do
            {
                result = generator.Next(0, 1000000).ToString("D6");
            } while (await _withdrawRepository.GetByWithdrawalNumber(result) != null);
            return result;
        }
        public async Task<Withdrawal> GetById(Guid withdrawId)
        {
            return await _withdrawRepository.GetById(withdrawId);
        }
    }
}
