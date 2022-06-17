using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Domain.ViewModel
{
    public class UserPlanReportInfraViewModel
    {
        public string PlanName { get; set; }
        public int WalletType { get; set; }
        public int Duration { get; set; }
        public double InvestmentAmount { get; set; }
        public double AccruedProfit { get; set; }
        public int ProfitPercent { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
    }
}
