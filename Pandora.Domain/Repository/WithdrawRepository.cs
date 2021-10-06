using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pandora.Domain.Domain;

namespace Pandora.Domain.Repository
{
    public interface WithdrawalRepository
    {
        Task Add(Withdrawal referral);
        Task<Withdrawal> GetByWithdrawalNumber(string withdrawalNumber);
        Task<Withdrawal> GetById(Guid withdrawId);
    }
}
