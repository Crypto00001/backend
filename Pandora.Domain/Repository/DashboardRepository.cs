using System;
using System.Collections.Generic;
using Pandora.Domain.Domain;

namespace Pandora.Domain.Repository
{
    public interface DashboardRepository
    {
        List<Dashboard> Get();

    }
}
