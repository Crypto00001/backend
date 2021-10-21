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
    public interface IWalletService
    {
        Task<IEnumerable<WalletViewModel>> GetAll(Guid userId);
        Task<IEnumerable<WalletForDepositViewModel>> GetAllForDeposit(Guid userId);
        Task<IEnumerable<WalletForWithdrawViewModel>> GetAllForWithdraw(Guid userId);
    }
}
