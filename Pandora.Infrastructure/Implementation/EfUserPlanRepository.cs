using Microsoft.EntityFrameworkCore;
using Pandora.Domain.Domain;
using Pandora.Domain.Repository;
using Pandora.Domain.ViewModel;
using Pandora.Infrastructure.Base;
using Pandora.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora.Infrastructure.Implementation
{
    public class EfUserPlanRepository : EFRepository<UserPlan>, UserPlanRepository
    {
        public EfUserPlanRepository(EFDbContext context) : base(context)
        {
        }

        public async Task<List<UserPlanReportInfraViewModel>> GetAll(Guid userId)
        {
            var query = from userPlan in _context.Set<UserPlan>()
                        join plan in _context.Set<Plan>() on userPlan.PlanId equals plan.Id
                        where userPlan.UserId == userId
                        select new UserPlanReportInfraViewModel()
                        {
                            AccruedProfit = userPlan.AccruedProfit,
                            Duration = plan.Duration,
                            IsActive = userPlan.IsActive,
                            InvestmentAmount = userPlan.InvestmentAmount,
                            PlanName = plan.Name,
                            ProfitPercent = 10,
                            WalletType = userPlan.WalletType,
                            StartDate = userPlan.CreateDate
                        };
            return await query.ToListAsync();
        }
    }
}
