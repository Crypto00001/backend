using Pandora.Domain.Domain;
using Pandora.Domain.Repository;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using Pandora.Application.Contract;
using System;
using System.Linq;
using Pandora.Application.ViewModel;
using System.Threading.Tasks;
using Pandora.Application.Command.UserPlans;

namespace Pandora.Application.Service
{
    public class UserPlanService : IUserPlanService
    {
        private readonly UserPlanRepository _userPlanRepository;
        private readonly PlanRepository _planRepository;
        private readonly WalletRepository _walletRepository;
        public UserPlanService(UserPlanRepository userPlanRepository, PlanRepository planRepository, WalletRepository walletRepository)
        {
            _userPlanRepository = userPlanRepository;
            _planRepository = planRepository;
            _walletRepository = walletRepository;
        }

        public async Task CreateAsync(CreateUserPlanCommand command, Guid userId)
        {
            var wallet = await _walletRepository.GetUserWalletBalanceByType(userId, (int)command.WalletType);
            if (wallet.AvailableBalance > command.InvestmentAmount)
                throw new AppException("There is not enough balance");

            UserPlan userPlan = new UserPlan()
            {
                AccruedProfit = 0,
                InvestmentAmount = command.InvestmentAmount,
                IsActive = true,
                UserId = userId,
                PlanId = await _planRepository.GetPlanByName(command.PlanName)
            };

            await _userPlanRepository.Add(userPlan);

            wallet.AvailableBalance -= command.InvestmentAmount;
            wallet.InvestedBalance += command.InvestmentAmount;
            await _walletRepository.Update(wallet);
        }

        public Task<UserPlan> GetById(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
