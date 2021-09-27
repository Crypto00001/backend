using Pandora.Domain.Base;
using System;

namespace Pandora.Domain.Domain
{
    public class Wallet : BaseEntity
    {
        public Guid UserId { get; set; }
        public int Type { get; set; }
        public string Address { get; set; }
        public decimal Balance { get; set; }
        public decimal InvestedBalance { get; set; }
        public decimal AvailableBalance { get; set; }
        
    }
}
