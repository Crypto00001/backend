using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pandora.Domain.Domain;

namespace Pandora.Domain.Repository
{
    public interface WalletRepository
    {
        Task<List<Wallet>> GetAll(Guid userId);
        Task<Wallet> GetUserWalletBalanceByType(Guid userId,int walletType);
        Task Update(Wallet wallet);
    }
}
