using Microsoft.EntityFrameworkCore;
using Pandora.Domain.Domain;
using Pandora.Domain.Repository;
using Pandora.Infrastructure.Base;
using Pandora.Infrastructure.Context;
using System;
using System.Threading.Tasks;

namespace Pandora.Infrastructure.Implementation
{
    public class EfUserPlanRepository : EFRepository<UserPlan>, UserPlanRepository
    {
        public EfUserPlanRepository(EFDbContext context) : base(context)
        {
        }
    }
}
