namespace Pandora.Application.ViewModel
{
    public class WalletForDepositViewModel
    {
        public string Name { get; set; }
        public int WalletType { get; set; }
        public string WalletAddress { get; set; }
        public double Balance { get; set; }
        public double InvestedBalance { get; set; }
    }
}
