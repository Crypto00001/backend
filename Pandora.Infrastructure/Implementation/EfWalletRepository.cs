﻿using Microsoft.EntityFrameworkCore;
using Pandora.Domain.Domain;
using Pandora.Domain.Repository;
using Pandora.Infrastructure.Base;
using Pandora.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora.Infrastructure.Implementation
{
    public class EfWalletRepository : EFRepository<Wallet>, WalletRepository
    {
        public EfWalletRepository(EFDbContext context) : base(context)
        {
        }

        public Task<List<Wallet>> GetAll(Guid userId)
        {
            return _context.Set<Wallet>().Where(q => q.UserId == userId).ToListAsync();
        }

        public async Task<Wallet> GetUserWalletBalanceByType(Guid userId, int walletType)
        {
           return await _context.Set<Wallet>().Where(q => q.UserId == userId && q.Type == walletType).FirstOrDefaultAsync();
        }
    }
}