using Microsoft.EntityFrameworkCore;
using Pandora.Domain.Domain;
using Pandora.Domain.Repository;
using Pandora.Infrastructure.Base;
using Pandora.Infrastructure.Context;
using System;
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
    }
}
