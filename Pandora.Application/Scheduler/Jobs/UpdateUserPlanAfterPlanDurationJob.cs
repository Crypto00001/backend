using Pandora.Application.Contract;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pandora.Domain.Repository;

namespace Pandora.Application.Scheduler.Jobs
{
    public class UpdateUserPlanAfterPlanDurationJob : IJob
    {
        private readonly UserPlanRepository _userPlanRepository;
        private readonly WalletRepository _walletRepository;

        public UpdateUserPlanAfterPlanDurationJob(UserPlanRepository userPlanRepository,
            WalletRepository walletRepository)
        {
            _userPlanRepository = userPlanRepository;
            _walletRepository = walletRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var userPlans = await _userPlanRepository.GetAllActivePlans();
                foreach (var userPlan in userPlans)
                {
                    if (userPlan.EndDate < DateTime.Now)
                    {
                        userPlan.IsActive = false;

                        var wallet = await _walletRepository.GetUserWalletByType(userPlan.UserId, userPlan.WalletType);
                        if (wallet != null)
                        {
                            var netProfit =
                                (double)Convert.ToDecimal(userPlan.AccruedProfit - userPlan.InvestmentAmount);
                            wallet.InvestedBalance =
                                (double)Convert.ToDecimal(wallet.InvestedBalance - userPlan.InvestmentAmount);
                            wallet.AvailableBalance =
                                (double)Convert.ToDecimal(wallet.AvailableBalance + userPlan.AccruedProfit);
                            wallet.Balance = (double)Convert.ToDecimal(wallet.Balance + netProfit);

                            await _walletRepository.Update(wallet);
                            await _userPlanRepository.Update(userPlan);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}