namespace Pandora.Application.ViewModel
{
    public class WalletForDepositViewModel
    {
        public string Name { get; set; }
        public int WalletType { get; set; }
        public string WalletAddress { get; set; }
        public decimal Balance { get; set; }
        public decimal InvestedBalance { get; set; }
    }
}
