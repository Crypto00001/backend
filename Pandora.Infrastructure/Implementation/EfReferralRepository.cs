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
        public async Task<bool> HasReferralByEmail(string email)
        {
            return await _context.Set<Referral>().AnyAsync(q => q.Email == email);
        }

        public async Task<Referral> GetByReferralCode(string referralCode)
        {
            return await _context.Set<Referral>().FirstOrDefaultAsync(q => q.ReferralCode == referralCode);
        }

        public async Task<bool> IsReferralLimitationFull(Guid userId)
        {
            return await _context.Set<Referral>().CountAsync(q => q.UserId == userId) >= 5;
        }

        public async Task<List<Referral>> GetAll(Guid userId)
        {
            return await _context.Set<Referral>().Where(q => q.UserId == userId).ToListAsync();
        }
    }
}
