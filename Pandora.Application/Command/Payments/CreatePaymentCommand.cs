using Pandora.Domain.Domain;
using System.ComponentModel.DataAnnotations;

namespace Pandora.Application.Command.Payments
{
    public class CreatePaymentCommand
    {
        [Required]
        public int WalletType { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string TransactionId { get; set; }

    }
}
