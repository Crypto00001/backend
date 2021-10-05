using Pandora.Domain.Domain;
using Pandora.Domain.Repository;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using Pandora.Application.Contract;
using System;
using System.Linq;
using Pandora.Application.ViewModel;
using System.Threading.Tasks;
using Pandora.Application.Enums;
using Pandora.Application.Command.Payments;

namespace Pandora.Application.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly PaymentRepository _paymentRepository;

        public PaymentService(PaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task CreateAsync(CreatePaymentCommand command, Guid userId)
        {

            Payment payment = new Payment
            {
                Amount = command.Amount,
                PaymentNumber = "",
                TransactionId = command.TransactionId,
                UserId = userId,
                WalletType = command.WalletType
            };
            await _paymentRepository.Add(payment);
        }

        public async Task<Payment> GetById(Guid paymentId)
        {
            return await _paymentRepository.GetById(paymentId);
        }
    }
}
