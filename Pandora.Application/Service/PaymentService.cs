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
        private readonly WalletRepository _walletRepository;

        public PaymentService(PaymentRepository paymentRepository, WalletRepository walletRepository)
        {
            _paymentRepository = paymentRepository;
            _walletRepository = walletRepository;
        }

        public async Task<PaymentViewModel> CreateAsync(CreatePaymentCommand command, Guid userId)
        {

            Payment payment = new Payment
            {
                Amount = command.Amount,
                PaymentNumber = await GetPaymentNumberAsync(),
                TransactionId = command.TransactionId,
                UserId = userId,
                WalletType = command.WalletType
            };
            await _paymentRepository.Add(payment);
            
            var wallet = await _walletRepository.GetUserWalletBalanceByType(userId, command.WalletType);
            wallet.AvailableBalance+=command.Amount;
            wallet.Balance+=command.Amount;
            await _walletRepository.Update(wallet);
            
            return new PaymentViewModel(){
                PaymentNumber = payment.PaymentNumber
            };
        }

        private async Task<string> GetPaymentNumberAsync()
        {
            Random generator = new Random();
            string result;
            do
            {
                result = generator.Next(0, 1000000).ToString("D6");
            } while (await _paymentRepository.GetByPaymentNumber(result) != null);
            return result;
        }

        public async Task<Payment> GetById(Guid paymentId)
        {
            return await _paymentRepository.GetById(paymentId);
        }
        
    }
}
