using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pandora.Domain.Domain;

namespace Pandora.Domain.Repository
{
    public interface DashboardRepository
    {
        Task<List<Dashboard>> GetAll();

    }
}
