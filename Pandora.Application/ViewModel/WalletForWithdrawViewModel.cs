namespace Pandora.Application.ViewModel
{
    public class WalletForWithdrawViewModel
    {
        public string Name { get; set; }
        public int WalletType { get; set; }
        public decimal Balance { get; set; }
        public decimal AvailableBalance { get; set; }
    }
}
