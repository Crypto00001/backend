using System;

namespace Pandora.Application.CheckPaymentManager
{
    public class GeneralCheckPaymentManager : ICheckPaymentManager
    {
        private string TransactionId { get; }

        public GeneralCheckPaymentManager(string transactionId)
        {
            TransactionId = transactionId;
        }
        public ResultCheckPaymentModel CheckResult()
        {
            throw new NotImplementedException();
        }
    }   
}