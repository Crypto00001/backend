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
using Pandora.Application.Scraper;

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
            var wallet = await _walletRepository.GetUserWalletByType(userId, (int)command.WalletType);
            if (command.InvestmentAmount > wallet.AvailableBalance)
                throw new AppException("There is not enough balance");
            var plan = await _planRepository.GetPlanByName(command.PlanName);
            if (plan == null)
                throw new AppException("This plan does not exist");

            double latestPrice = 0;
            switch (wallet.Type)
            {
                case (int)WalletType.Bitcoin:
                    latestPrice = UpdatePriceScraper.BitcoinPrice;
                    break;
                case (int)WalletType.Ethereum:
                    latestPrice = UpdatePriceScraper.EthereumPrice;
                    break;
                case (int)WalletType.Litecoin:
                    latestPrice = UpdatePriceScraper.LitecoinPrice;
                    break;
                case (int)WalletType.Zcash:
                    latestPrice = UpdatePriceScraper.ZCashPrice;
                    break;                
                case (int)WalletType.Tether:
                    latestPrice = UpdatePriceScraper.TetherPrice;
                    break;
            }

            var dollarAInvestedAmount = command.InvestmentAmount * latestPrice;
            if (dollarAInvestedAmount < plan.MinimumDeposit)
                throw new AppException("The minimum invested amount for this plain is {0} dollars",
                    plan.MinimumDeposit);
            var referralCount = await _refferalRepository.GetActiveInviteesCount(userId);
            var planPercentage = plan.ProfitPercent / 100;
            var referralProfitPercentage = referralCount * ((double)plan.ReferralPercent / 100);
            var accruedProfit=(double)(command.InvestmentAmount * Math.Pow(((1+ (planPercentage + referralProfitPercentage)/1)),plan.Duration));
            UserPlan userPlan = new UserPlan()
            {
                AccruedProfit = accruedProfit,
                WalletType = wallet.Type,
                InvestmentAmount = command.InvestmentAmount,
                ProfitPercentage = plan.ProfitPercent,
                ReferralProfitPercentage = referralProfitPercentage,
                IsActive = true,
                UserId = userId,
                PlanId = plan.Id,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(plan.Duration)
            };
            
            await _userPlanRepository.Add(userPlan);


            wallet.AvailableBalance = (double)Convert.ToDecimal(wallet.AvailableBalance - command.InvestmentAmount);
            wallet.InvestedBalance = (double)Convert.ToDecimal(wallet.InvestedBalance + command.InvestmentAmount);
            await _walletRepository.Update(wallet);

            var user = await _userRepository.GetById(userId);
            if (user != null && !user.HasInvested)
            {
                user.HasInvested = true;
                await _userRepository.Update(user);
            }
        }

        public async Task<IEnumerable<UserPlanReportViewModel>> GetAll(Guid userId)
        {
            return (await _userPlanRepository.GetAll(userId)).Select(q => new UserPlanReportViewModel
            {
                CurrencyName = Enum.GetName(typeof(WalletType), q.WalletType),
                AccruedProfit = q.AccruedProfit,
                StartDate = q.StartDate.ToString("yyyy/MM/dd"),
                InvestmentAmount = q.InvestmentAmount,
                IsActive = q.IsActive,
                EndDate = q.EndDate.ToString("yyyy/MM/dd"),
                PlanName = q.PlanName,
                Progress = (int)Math.Floor(((DateTime.Now - q.StartDate).TotalDays /
                                            (q.StartDate.AddMonths(q.Duration) - q.StartDate).TotalDays) * 100),
                ProfitPercent = q.ProfitPercent
            }).OrderByDescending(o => o.StartDate);
        }
        
    }
}