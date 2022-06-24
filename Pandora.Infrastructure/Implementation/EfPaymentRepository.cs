using Microsoft.EntityFrameworkCore;
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
    public class EfPaymentRepository : EFRepository<Payment>, PaymentRepository
    {
        public EfPaymentRepository(EFDbContext context) : base(context)
        {
        }
        public async Task<Payment> GetByPaymentNumber(string paymentNumber)
        {
           return await _context.Set<Payment>().FirstOrDefaultAsync(q => q.PaymentNumber == paymentNumber);
        }

        public async Task<bool> HasNotDupicateTransactionId(string transactionId)
        {
            return await _context.Set<Payment>().AnyAsync(q => q.TransactionId == transactionId && q.IsPaid);
        }
    }
}
