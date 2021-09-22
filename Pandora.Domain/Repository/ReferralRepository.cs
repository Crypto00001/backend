using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pandora.Domain.Domain;

namespace Pandora.Domain.Repository
{
    public interface ReferralRepository
    {
        Task Add(Referral referral);
        Task<Referral> GetByReferralCode(string referralCode);
        Task<bool> HasReferralByEmail(string email);
        Task Remove(Guid UserId);
    }
}
