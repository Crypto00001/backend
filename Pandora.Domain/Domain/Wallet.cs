using Pandora.Domain.Base;
using System;

namespace Pandora.Domain.Domain
{
    public class Wallet : BaseEntity
    {
        public Guid UserId { get; set; }
        public int WalletType { get; set; }
        public string WalletAddress { get; set; }
        public decimal Balance { get; set; }
        public decimal InvestedBalance { get; set; }
        public decimal AvailableBalance { get; set; }
        
    }
}
