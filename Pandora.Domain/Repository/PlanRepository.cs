using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pandora.Domain.Domain;

namespace Pandora.Domain.Repository
{
    public interface PlanRepository
    {
        Task<Plan> GetPlanByName(string planName);
        Task<List<Plan>> GetAll();
    }
}
