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
                case WalletType.Bitcoin:
                case WalletType.Ethereum:
                case WalletType.Litecoin:
                case WalletType.Tether:
                    return new GeneralCheckPaymentManager(transactionId);
                default:
                    return null;
            }
        }
    }
}