using Pandora.Domain.Domain;
using Pandora.Domain.Repository;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using Pandora.Application.Contract;
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

        public async Task<Plan> GetByName(string planName)
        {
            return await _planRepository.GetPlanByName(planName);
        }

        public async Task<List<PlanViewModel>> GetAll()
        {
            return (await _planRepository.GetAll()).Select(q => new PlanViewModel()
            {
                Name = q.Name,
                ProfitPercent = q.ProfitPercent,
                ReferralPercent = q.ReferralPercent,
                MinimumDeposit = q.MinimumDeposit,
                Duration = q.Duration
            }).ToList();
        }
    }
}
