using Pandora.Domain.Base;
using System.Collections.Generic;

namespace Pandora.Domain.Domain
{
    public class Plan : BaseEntity
    {
        public string Name { get; set; }
        public int ProfitPercent { get; set; }
        public int MinimumDeposit { get; set; }
        public int Duration { get; set; }

    }
}
