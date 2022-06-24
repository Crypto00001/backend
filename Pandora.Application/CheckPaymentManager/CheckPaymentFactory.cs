using Pandora.Application.Enums;

namespace Pandora.Application.CheckPaymentManager
{
    public class CheckPaymentFactory
    {
        public ICheckPaymentManager GetResult(string transactionId, int walletType)
        {
            switch ((WalletType)walletType)
            {
                case WalletType.Zcash:
                    return new ZCashCheckPaymentManager(transactionId);
                case WalletType.Bitcoin:
                case WalletType.Etherium:
                case WalletType.Litecoin:
                    return new GeneralCheckPaymentManager(transactionId);
                default:
                    return null;
            }
        }
    }
}