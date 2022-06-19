using Pandora.Domain.Base;
using System;
using System.Collections.Generic;

namespace Pandora.Domain.Domain
{
    public class Referral : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid InvitedUserId { get; set; }
    }
}
