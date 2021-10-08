using Pandora.Application.Command.Referrals;
using Pandora.Application.ViewModel;
using Pandora.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Application.Contract
{
    public interface IReferralService
    {
        Task CreateAsync(CreateReferralCommand command, Guid userId);
        Task<List<ReferralViewModel>> GetAll(Guid userId);
        Task<int> GetActiveInviteesCount(Guid userId);
    }
}
