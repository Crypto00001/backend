using Pandora.Domain.Domain;
using Pandora.Domain.Repository;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using Pandora.Application.Contract;
using System;
using System.Linq;
using Pandora.Application.ViewModel;
using System.Threading.Tasks;

namespace Pandora.Application.Service
{
    public class PlanService : IPlanService
    {
        private readonly PlanRepository _planRepository;
        public PlanService(PlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<Guid> GetByName(string planName)
        {
            return await _planRepository.GetPlanByName(planName);
        }
    }
}
