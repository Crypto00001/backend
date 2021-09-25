using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pandora.Domain.Domain;

namespace Pandora.Domain.Repository
{
    public interface UserPlanRepository
    {
        Task Add(UserPlan referral);
    }
}
