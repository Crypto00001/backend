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
        Task<int> GetActiveInviteesCount(Guid userId);
        Task<List<User>> GetActiveInvitees(Guid userId);
        Task<bool> IsReferralLimitationFull(Guid userId);
        Task<List<Referral>> GetAll(Guid userId);
        Task<List<Referral>> GetAllInvitedUser(Guid invitedUserId);
        Task Remove(Guid userId);
    }
}
