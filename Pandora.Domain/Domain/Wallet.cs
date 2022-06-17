using Pandora.Domain.Base;
using System;

namespace Pandora.Domain.Domain
{
    public class Wallet : BaseEntity
    {
        public Guid UserId { get; set; }
        public int Type { get; set; }
        public string Address { get; set; }
        public double Balance { get; set; }
        public double InvestedBalance { get; set; }
        public double AvailableBalance { get; set; }
        
    }
}
