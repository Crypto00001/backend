using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Application.ViewModel
{
    public class PlanViewModel
    {
        public string Name { get; set; }
        public double ProfitPercent { get; set; }
        public double ReferralPercent { get; set; }
        public int MinimumDeposit { get; set; }
        public int Duration { get; set; }
    }
}
