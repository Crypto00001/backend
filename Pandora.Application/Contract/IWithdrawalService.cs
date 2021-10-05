using Pandora.Application.Command.Withdraws;
using Pandora.Application.ViewModel;
using Pandora.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Application.Contract
{
    public interface IWithdrawalService
    {
        Task CreateAsync(CreateWithdrawalCommand command, Guid userId);
        Task<Withdrawal> GetById(Guid userId);
    }
}
