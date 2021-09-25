namespace Pandora.Application.ViewModel
{
    public class WalletViewModel
    {
        public int WalletType { get; set; }
        public string WalletAddress { get; set; }
        public decimal Balance { get; set; }
        public decimal InvestedBalance { get; set; }
        public decimal AvailableBalance { get; set; }
    }
}
