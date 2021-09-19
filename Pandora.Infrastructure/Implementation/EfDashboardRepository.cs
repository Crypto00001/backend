using Pandora.Domain.Domain;
using Pandora.Domain.Repository;
using Pandora.Infrastructure.Base;
using Pandora.Infrastructure.Context;
using System;
using System.Linq;

namespace Pandora.Infrastructure.Implementation
{
    public class EfDashboardRepository : EFRepository<Dashboard>, DashboardRepository
    {
        public EfDashboardRepository(EFDbContext context) : base(context)
        {
        }
    }
}
