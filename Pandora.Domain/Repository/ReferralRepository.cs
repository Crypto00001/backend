using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pandora.Domain.Domain;

namespace Pandora.Domain.Repository
{
    public interface ReferralRepository
    {
        Task Add(Referral referral);
        Task Update(Referral referral);
        Task<Referral> GetByReferralCode(string referralCode);
        Task<bool> HasReferralByEmail(string email);
        Task<int> GetActiveInviteesCount(Guid userId);
        Task<Referral> GetReferralByEmail(string email);
        Task<bool> IsReferralLimitationFull(Guid userId);
        Task<List<Referral>> GetAll(Guid userId);
        Task Remove(Guid UserId);
    }
}
