using System;

namespace Pandora.Application.ViewModel
{
    public class UserPlanReportViewModel
    {
        public string PlanName { get; set; }
        public string CurrencyName { get; set; }
        public double InvestmentAmount { get; set; }
        public double AccruedProfit { get; set; }
        public int ProfitPercent { get; set; }
        public bool IsActive { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Progress { get; set; }
    }
}
