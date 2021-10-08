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
using Pandora.Application.Enums;

namespace Pandora.Application.Service
{
    public class UserPlanService : IUserPlanService
    {
        private readonly UserPlanRepository _userPlanRepository;
        private readonly UserRepository _userRepository;
        private readonly PlanRepository _planRepository;
        private readonly WalletRepository _walletRepository;
        private readonly ReferralRepository _refferalRepository;
        public UserPlanService(UserPlanRepository userPlanRepository, 
        PlanRepository planRepository, 
        UserRepository userRepository, 
        ReferralRepository referralRepository, 
        WalletRepository walletRepository)
        {
            _userPlanRepository = userPlanRepository;
            _userRepository = userRepository;
            _planRepository = planRepository;
            _walletRepository = walletRepository;
            _refferalRepository = referralRepository;
        }

        public async Task CreateAsync(CreateUserPlanCommand command, Guid userId)
        {
            var wallet = await _walletRepository.GetUserWalletBalanceByType(userId, (int)command.WalletType);
            if (command.InvestmentAmount > wallet.AvailableBalance)
                throw new AppException("There is not enough balance");
            var planId = await _planRepository.GetPlanByName(command.PlanName);
            if (planId == Guid.Empty)
                throw new AppException("This plan does not exist");

            UserPlan userPlan = new UserPlan()
            {
                AccruedProfit = 0,
                WalletType = wallet.Type,
                InvestmentAmount = command.InvestmentAmount,
                IsActive = true,
                UserId = userId,
                PlanId = planId
            };

            await _userPlanRepository.Add(userPlan);
            

            wallet.AvailableBalance -= command.InvestmentAmount;
            wallet.InvestedBalance += command.InvestmentAmount;
            await _walletRepository.Update(wallet);

            var user = await _userRepository.GetById(userId);
            var refferal = await _refferalRepository.GetReferralByEmail(user.Email);
            if(!refferal.HasInvested)
            {
                refferal.HasInvested = true;
                await _refferalRepository.Update(refferal);
            }
        }

        public async Task<IEnumerable<UserPlanReportViewModel>> GetAll(Guid userId)
        {
            return (await _userPlanRepository.GetAll(userId)).Select(q => new UserPlanReportViewModel
            {
                CurrencyName = Enum.GetName(typeof(WalletType), q.WalletType),
                AccruedProfit = q.AccruedProfit,
                StartDate = q.StartDate.ToString("yyyy-mm-dd"),
                InvestmentAmount = q.InvestmentAmount,
                IsActive = q.IsActive,
                EndDate = q.StartDate.AddMonths(q.Duration).ToString("yyyy-mm-dd"),
                PlanName = q.PlanName,
                Progress = (int)Math.Floor(((DateTime.Now - q.StartDate).TotalDays / (q.StartDate.AddMonths(q.Duration) - q.StartDate).TotalDays) * 100),
                ProfitPercent = q.ProfitPercent
            }).OrderByDescending(o => o.StartDate);
        }

    }
}
