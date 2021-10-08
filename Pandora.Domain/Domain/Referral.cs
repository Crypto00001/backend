using Pandora.Domain.Base;
using System;
using System.Collections.Generic;

namespace Pandora.Domain.Domain
{
    public class Referral : BaseEntity
    {
        public Guid UserId { get; set; }
        public string ReferralCode { get; set; }
        public string Email { get; set; }
        public bool HasInvested { get; set; }
    }
}
