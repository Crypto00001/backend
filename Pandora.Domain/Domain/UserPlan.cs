using Pandora.Domain.Base;
using System;

namespace Pandora.Domain.Domain
{
    public class UserPlan : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid PlanId { get; set; }
        public int WalletType { get; set; }
        public double ProfitPercentage { get; set; }
        public double ReferralProfitPercentage { get; set; }
        public decimal InvestmentAmount { get; set; }
        public decimal AccruedProfit { get; set; }
        public bool IsActive { get; set; }
        
    }
}
