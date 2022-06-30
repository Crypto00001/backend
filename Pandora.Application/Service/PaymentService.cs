using Pandora.Domain.Domain;
using Pandora.Domain.Repository;
using BCryptNet = BCrypt.Net.BCrypt;
using Pandora.Application.Contract;
using System;
using Pandora.Application.ViewModel;
using System.Threading.Tasks;
using Pandora.Application.Command.Payments;
using Pandora.Application.Scheduler;
using Pandora.Application.Scheduler.Jobs;
using Quartz;

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

        public async Task<PaymentViewModel> CreateAsync(CreatePaymentCommand command, Guid userId,
            CheckPaymentConfirmationScheduler scheduler)
        {
            string paymentNumber = await GetPaymentNumberAsync();
            var paymentId = Guid.NewGuid();
            
            Payment payment = new Payment
            {
                Id = paymentId,
                IsPaid = false,
                Amount = command.Amount,
                PaymentNumber = await GetPaymentNumberAsync(),
                TransactionId = command.TransactionId,
                UserId = userId,
                WalletType = command.WalletType
            };
            await _paymentRepository.Add(payment);
            await RunPaymentCheckerJob(payment, userId, scheduler);
            return new PaymentViewModel()
            {
                PaymentNumber = paymentNumber
            };
        }

        private async Task RunPaymentCheckerJob(Payment payment, Guid userId,
            CheckPaymentConfirmationScheduler scheduler)
        {
            IJobDetail job = JobBuilder.Create<CheckTransactionConfirmJob>()
                .WithIdentity(Guid.NewGuid().ToString(), "CheckConfirmationJob")
                .UsingJobData("paymentId", payment.Id)
                .UsingJobData("userId", userId)
                .UsingJobData("transactionId", payment.TransactionId)
                .UsingJobData("amount", payment.Amount)
                .UsingJobData("walletType", payment.WalletType)
                .UsingJobData("fromAddress",
                    (await _walletRepository.GetUserWalletByType(userId, payment.WalletType)).Address)
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(Guid.NewGuid().ToString(), "CheckConfirmationGroup")
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(10)
                    .WithRepeatCount(12))
                .ForJob(job)
                .Build();
            await scheduler.GetScheduler().ScheduleJob(job, trigger);
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