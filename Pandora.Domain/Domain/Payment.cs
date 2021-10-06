﻿using Pandora.Domain.Base;
using System;

namespace Pandora.Domain.Domain
{
    public class Payment : BaseEntity
    {
        public Guid UserId { get; set; }
        public int WalletType { get; set; }
        public decimal Amount { get; set; }
        public string TransactionId { get; set; }
        public string PaymentNumber { get; set; }
        
    }
}