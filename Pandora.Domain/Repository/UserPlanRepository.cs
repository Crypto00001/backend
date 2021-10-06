using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pandora.Domain.Domain;
using Pandora.Domain.ViewModel;

namespace Pandora.Domain.Repository
{
    public interface UserPlanRepository
    {
        Task Add(UserPlan referral);
        Task<List<UserPlanReportInfraViewModel>> GetAll(Guid userId);
    }
}
