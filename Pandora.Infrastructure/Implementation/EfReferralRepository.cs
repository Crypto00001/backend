using Microsoft.EntityFrameworkCore;
using Pandora.Domain.Domain;
using Pandora.Domain.Repository;
using Pandora.Infrastructure.Base;
using Pandora.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora.Infrastructure.Implementation
{
    public class EfReferralRepository : EFRepository<Referral>, ReferralRepository
    {
        public EfReferralRepository(EFDbContext context) : base(context)
        {
        }
        // public async Task<bool> HasReferralByEmail(string email)
        // {
        //     return await _context.Set<Referral>().AnyAsync(q => q.Email == email);
        // }
        // public async Task<Referral> GetReferralByEmail(string email)
        // {
        //     return await _context.Set<Referral>().FirstOrDefaultAsync(q => q.Email == email);
        // }
        public async Task<int> GetActiveInviteesCount(Guid userId)
        {
            return await (from referral in _context.Set<Referral>().Where(q=>q.UserId == userId)
                join user in _context.Set<User>() on referral.InvitedUserId equals user.Id
                where user.HasInvested
                select referral).CountAsync();
        }
        public async Task<List<User>> GetActiveInvitees(Guid userId)
        {
            return await (from referral in _context.Set<Referral>().Where(q => q.UserId == userId)
                join user in _context.Set<User>() on referral.InvitedUserId equals user.Id
                select user).ToListAsync();
        }
        public async Task<bool> IsReferralLimitationFull(Guid userId)
        {
            return await _context.Set<Referral>().CountAsync(q => q.UserId == userId) >= 5;
        }

        public async Task<List<Referral>> GetAll(Guid userId)
        {
            return await _context.Set<Referral>().Where(q => q.UserId == userId).ToListAsync();
        }
        public async Task<List<Referral>> GetAllInvitedUser(Guid invitedUserId)
        {
            return await _context.Set<Referral>().Where(q => q.InvitedUserId == invitedUserId).ToListAsync();
        }
    }
}
