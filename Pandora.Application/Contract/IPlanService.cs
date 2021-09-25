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
    public interface IPlanService
    {
        Task<Guid> GetByName(string planName);
    }
}
