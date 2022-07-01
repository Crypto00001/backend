using Quartz;
using System;
using System.Threading.Tasks;
using Pandora.Application.CheckPaymentManager;
using Pandora.Application.Enums;
using Pandora.Domain.Domain;
using Pandora.Domain.Repository;

namespace Pandora.Application.Scheduler.Jobs
{
    public class CheckTransactionConfirmJob : IJob
    {
        private readonly PaymentRepository _paymentRepository;
        private readonly WalletRepository _walletRepository;

        public CheckTransactionConfirmJob(PaymentRepository paymentRepository, WalletRepository walletRepository)
        {
            _paymentRepository = paymentRepository;
            _walletRepository = walletRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var key = context.JobDetail.Key;

                var dataMap = context.MergedJobDataMap;

                var userId = dataMap.GetGuidValue("userId");
                var paymentId = dataMap.GetGuidValue("paymentId");
                var transactionId = dataMap.GetString("transactionId");
                var claimedAmount = dataMap.GetDoubleValue("amount");
                var walletType = dataMap.GetInt("walletType");
                var fromAddress = dataMap.GetString("fromAddress");
                var paymentFactory = new CheckPaymentFactory();
                var crypto = paymentFactory.GetResult(transactionId, walletType);
                var result = crypto.CheckResult();
                if (result is { IsConfirmed: true })
                {
                    if (result.PaymentAmount == Convert.ToDecimal(claimedAmount) &&
                        result.FromAddress == fromAddress &&
                        result.ToAddress == GetDestinationAddress(walletType))
                    {
                        if (!await _paymentRepository.HasNotDupicateTransactionId(transactionId))
                        {
                            var payment = await _paymentRepository.GetById(paymentId);
                            payment.IsPaid = true;
                            await _paymentRepository.Update(payment);

                            var wallet = await _walletRepository.GetUserWalletByType(userId, walletType);
                            wallet.AvailableBalance += claimedAmount;
                            wallet.Balance += claimedAmount;
                            await _walletRepository.Update(wallet);

                            await context.Scheduler.DeleteJob(key);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public string GetDestinationAddress(int walletType)
        {
            switch ((WalletType)walletType)
            {
                case WalletType.Zcash:
                    return "yyy";
                case WalletType.Bitcoin:
                    return "yyy";
                case WalletType.Ethereum:
                    return "yyy";
                case WalletType.Litecoin:
                    return "yyy";
                case WalletType.Tether:
                    return "yyy";
                default:
                    return null;
            }
        }
    }
}