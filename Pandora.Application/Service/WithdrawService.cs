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
using Pandora.Application.Command.Withdraws;

namespace Pandora.Application.Service
{
    public class WithdrawalService : IWithdrawalService
    {
        private readonly WithdrawalRepository _withdrawRepository;

        public WithdrawalService(WithdrawalRepository withdrawRepository)
        {
            _withdrawRepository = withdrawRepository;
        }

        public async Task CreateAsync(CreateWithdrawalCommand command, Guid userId)
        {

            Withdrawal withdrawal = new Withdrawal
            {
                Amount = command.Amount,
                WithdrawNumber = "",
                UserId = userId,
                WalletType = command.WalletType
            };
            await _withdrawRepository.Add(withdrawal);
        }

        public async Task<Withdrawal> GetById(Guid withdrawId)
        {
            return await _withdrawRepository.GetById(withdrawId);
        }
    }
}
