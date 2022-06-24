using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pandora.Domain.Domain;

namespace Pandora.Domain.Repository
{
    public interface PaymentRepository
    {
        Task Add(Payment referral);
        Task Update(Payment wallet);
        Task<Payment> GetByPaymentNumber(string paymentNumber);
        Task<Payment> GetById(Guid paymentId);
        Task<bool> HasNotDupicateTransactionId(string transactionId);
    }
}
