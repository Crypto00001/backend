using System;

namespace Pandora.Application.CheckPaymentManager
{
    public class ZCashCheckPaymentManager : ICheckPaymentManager
    {
        private string TransactionId { get; }

        public ZCashCheckPaymentManager(string transactionId)
        {
            TransactionId = transactionId;
        }

        public ResultCheckPaymentModel CheckResult()
        {
            throw new NotImplementedException();
        }
    }
}