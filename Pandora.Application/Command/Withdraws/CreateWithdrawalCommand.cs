using Pandora.Domain.Domain;
using System.ComponentModel.DataAnnotations;

namespace Pandora.Application.Command.Withdraws
{
    public class CreateWithdrawalCommand
    {
        [Required]
        public int WalletType { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public string WalletAddress { get; set; }

    }
}
