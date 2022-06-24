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
    public class EfPlanRepository : EFRepository<Plan>, PlanRepository
    {
        public EfPlanRepository(EFDbContext context) : base(context)
        {
        }

        public async Task<Plan> GetPlanByName(string planName)
        {
            return await _context.Set<Plan>().Where(q => q.Name == planName).FirstOrDefaultAsync();
        }
    }
}
