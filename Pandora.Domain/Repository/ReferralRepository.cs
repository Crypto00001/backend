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
        //Task<bool> HasReferralByEmail(string email);
        Task<int> GetActiveInviteesCount(Guid userId);
        //Task<Referral> GetReferralByEmail(string email);
        Task<bool> IsReferralLimitationFull(Guid userId);
        Task<List<Referral>> GetAll(Guid userId);
        Task<List<Referral>> GetAllInvitedUser(Guid invitedUserId);
        Task Remove(Guid userId);
    }
}
