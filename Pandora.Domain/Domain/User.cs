using Pandora.Domain.Base;
using System;
using System.Collections.Generic;

namespace Pandora.Domain.Domain
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public DateTime? LastLoginAttemptAt { get; set; }
        public int LoginFailedAttemptsCount { get; set; }
        public string UserReferralCode { get; set; }
        public string ReferralCode { get; set; }
        public string ResetPasswordCode { get; set; }
        public bool HasInvested { get; set; }
    }
}
