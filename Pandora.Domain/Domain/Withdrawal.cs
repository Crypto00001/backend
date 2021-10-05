using Pandora.Domain.Base;
using System;

namespace Pandora.Domain.Domain
{
    public class Withdrawal : BaseEntity
    {
        public Guid UserId { get; set; }
        public int WalletType { get; set; }
        public decimal Amount { get; set; }
        public string WithdrawNumber { get; set; }

    }
}
